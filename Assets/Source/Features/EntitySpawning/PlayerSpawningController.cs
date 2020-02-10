using Source.Entities.Config;
using Source.Features.DataBridge;
using Source.Features.ScreenSize;
using Unity.Mathematics;
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

            // ToDO [ECS] Player and floor size need to be gotten differently
            var playerSize = _entityConfig.PlayerEntityConfig
                .PlayerEntityPrefab
                .GetComponent<MeshFilter>()
                .sharedMesh
                .bounds
                .size;
            
            var bottomLeftCorner = _screenSizeController.GetBottomLeftCorner();

            var spawnPosition = new float3(
                bottomLeftCorner.x + playerSize.x,
                bottomLeftCorner.y + floorTileSize.y + playerSize.y / 2,
                0);

            _playerSpawner.SpawnPlayerAt(spawnPosition);

            Blackboard.Set(BlackboardEntryId.PlayerSpawnPosition, spawnPosition);
        }
    }
}
