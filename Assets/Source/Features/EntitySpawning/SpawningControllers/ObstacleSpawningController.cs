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
    public class ObstacleSpawningController : AbstractDisposable, IInitializable
    {
        private readonly ObstacleEntityFactory _obstacleEntityFactory;
        private readonly ObstacleEntityConfig _obstacleEntityConfig;
        private readonly ScreenSizeModel _screenSizeModel;

        public ObstacleSpawningController(
            ObstacleEntityFactory obstacleEntityFactory,
            ObstacleEntityConfig obstacleEntityConfig,
            ScreenSizeModel screenSizeModel)
        {
            _obstacleEntityFactory = obstacleEntityFactory;
            _obstacleEntityConfig = obstacleEntityConfig;
            _screenSizeModel = screenSizeModel;
        }

        public void Initialize()
        {
            Observable.Interval(TimeSpan.FromSeconds(2))
                .Subscribe(_ => Spawn())
                .AddTo(Disposer);
        }

        private void Spawn()
        {
            var rightBottomCorner = _screenSizeModel.GetCurrentRightBottomCorner();
            var spawnPosition = new float3(
                rightBottomCorner.x + 1,
                _obstacleEntityConfig.SpawnPositionY,
                0);

            _obstacleEntityFactory.CreateEntityAt(spawnPosition);
        }
    }
}
