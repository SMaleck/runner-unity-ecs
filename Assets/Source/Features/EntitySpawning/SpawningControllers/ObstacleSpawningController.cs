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
        private readonly ScreenSizeController _screenSizeController;

        public ObstacleSpawningController(
            ObstacleEntityFactory obstacleEntityFactory,
            ObstacleEntityConfig obstacleEntityConfig,
            ScreenSizeController screenSizeController)
        {
            _obstacleEntityFactory = obstacleEntityFactory;
            _obstacleEntityConfig = obstacleEntityConfig;
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

            _obstacleEntityFactory.CreateEntityAt(new float3(spawnPosition.x, spawnPosition.y, 0));
        }
    }
}
