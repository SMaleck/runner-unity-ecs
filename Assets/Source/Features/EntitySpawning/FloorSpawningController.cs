using Source.Entities.Config;
using Source.Features.ScreenSize;
using UGF.Util.UniRx;
using UnityEngine;
using Zenject;

namespace Source.Features.EntitySpawning
{
    public class FloorSpawningController : AbstractDisposable, IInitializable
    {
        private readonly IFloorSpawner _floorSpawner;
        private readonly ScreenSizeModel _screenSizeModel;
        private readonly ScreenSizeController _screenSizeController;

        private readonly Vector3 _floorTileSize;
        private float FloorTileHalfSizeX => _floorTileSize.x / 2f;
        private float FloorTileHalfSizeY => _floorTileSize.y / 2f;

        public FloorSpawningController(
            IFloorSpawner floorSpawner,
            EntityConfig entityConfig,
            ScreenSizeModel screenSizeModel,
            ScreenSizeController screenSizeController)
        {
            _floorSpawner = floorSpawner;
            _screenSizeModel = screenSizeModel;
            _screenSizeController = screenSizeController;

            _floorTileSize = entityConfig.GetEntitySetting(EntityType.Floor)
                .EntityMesh
                .bounds
                .size;
        }

        public void Initialize()
        {
            FillFloor();
        }

        private void FillFloor()
        {
            var startPosition = _screenSizeController.GetBottomLeftCorner(
                FloorTileHalfSizeX,
                FloorTileHalfSizeY);

            var desiredTileCount = _screenSizeModel.WidthUnits / _floorTileSize.x;
            for (var i = 0; i < desiredTileCount; i++)
            {
                var sizeOffset = i * _floorTileSize.x;
                _floorSpawner.SpawnFloorTileAt(
                    new Vector3(
                        startPosition.x + sizeOffset,
                        startPosition.y,
                        0));
            }
        }

        private void SpawnNext()
        {
            var spawnPosition = _screenSizeController.GetBottomRightCorner();
            _floorSpawner.SpawnFloorTileAt(spawnPosition);
        }
    }
}
