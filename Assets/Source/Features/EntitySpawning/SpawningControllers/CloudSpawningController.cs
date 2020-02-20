using Source.Features.EntitySpawning.Config;
using Source.Features.EntitySpawning.Factories;
using Source.Features.ScreenSize;
using System;
using UGF.Util.UniRx;
using UniRx;
using Unity.Mathematics;
using Zenject;

namespace Source.Features.EntitySpawning.SpawningControllers
{
    public class CloudSpawningController : AbstractDisposable, IInitializable
    {
        private readonly CloudEntityFactory _cloudEntityFactory;
        private readonly CloudEntityConfig _cloudEntityConfig;
        private readonly ScreenSizeModel _screenSizeModel;


        public CloudSpawningController(
            CloudEntityFactory cloudEntityFactory,
            CloudEntityConfig cloudEntityConfig,
            ScreenSizeModel screenSizeModel)
        {
            _cloudEntityFactory = cloudEntityFactory;
            _cloudEntityConfig = cloudEntityConfig;
            _screenSizeModel = screenSizeModel;
        }

        public void Initialize()
        {
            Observable.Interval(TimeSpan.FromSeconds(2f))
                .Subscribe(_ => Spawn())
                .AddTo(Disposer);
        }

        private void Spawn()
        {
            var rightEdgeX = _screenSizeModel.GetCurrentEdge(ScreenSide.Right).x;

            var spawnPosition = new float3(
                rightEdgeX + _cloudEntityConfig.ScreenOffsetX,
                GetRandomY(),
                0);

            _cloudEntityFactory.CreateEntityAt(spawnPosition);
        }

        private float GetRandomY()
        {
            return UnityEngine.Random.Range(
                _cloudEntityConfig.PositionRangeMinY,
                _cloudEntityConfig.PositionRangeMaxY);
        }
    }
}
