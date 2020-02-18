using Source.Features.DataBridge;
using Source.Features.EntitySpawning.Config;
using Source.Features.EntitySpawning.Factories;
using Source.Features.ScreenSize;
using System;
using UGF.Util.UniRx;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Source.Features.EntitySpawning.SpawningControllers
{
    public class CloudSpawningController : AbstractDisposable, IInitializable
    {
        private readonly CloudEntityFactory _cloudEntityFactory;
        private readonly CloudEntityConfig _cloudEntityConfig;
        private readonly ScreenSizeController _screenSizeController;


        public CloudSpawningController(
            CloudEntityFactory cloudEntityFactory,
            CloudEntityConfig cloudEntityConfig,
            ScreenSizeController screenSizeController)
        {
            _cloudEntityFactory = cloudEntityFactory;
            _cloudEntityConfig = cloudEntityConfig;
            _screenSizeController = screenSizeController;
        }

        public void Initialize()
        {
            Observable.Interval(TimeSpan.FromSeconds(2))
                .Subscribe(_ => Spawn())
                .AddTo(Disposer);
        }

        private void Spawn()
        {
            var spawnPosition = _screenSizeController.GetBottomRightCorner(
                1,
                1.5f);

            _cloudEntityFactory.CreateEntityAt(new float3(spawnPosition.x, spawnPosition.y, 0));
        }
    }
}
