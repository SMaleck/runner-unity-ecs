using Source.Entities.Config;
using Source.Features.ScreenSize;
using UnityEngine;
using Zenject;

namespace Source.Features.EntitySpawning
{
    public class PlayerSpawningController : IInitializable
    {
        private readonly IPlayerSpawner _playerSpawner;
        private readonly EntityConfig _entityConfig;
        private readonly ScreenSizeController _screenSizeController;

        public PlayerSpawningController(
            IPlayerSpawner playerSpawner,
            EntityConfig entityConfig,
            ScreenSizeController screenSizeController)
        {
            _playerSpawner = playerSpawner;
            _entityConfig = entityConfig;
            _screenSizeController = screenSizeController;
        }

        public void Initialize()
        {
            var floorTileSize = _entityConfig.GetEntitySetting(EntityType.Floor)
                .EntityMesh
                .bounds
                .size;

            var playerSize = _entityConfig.GetEntitySetting(EntityType.Player)
                .EntityMesh
                .bounds
                .size;

            var bottomLeftCorner = _screenSizeController.GetBottomLeftCorner();

            _playerSpawner.SpawnPlayerAt(new Vector3(
                bottomLeftCorner.x + playerSize.x,
                bottomLeftCorner.y + floorTileSize.y + playerSize.y / 2));
        }
    }
}
