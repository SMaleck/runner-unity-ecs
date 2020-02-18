using Source.Features.DataBridge;
using Source.Features.EntitySpawning.Config;
using Source.Features.EntitySpawning.Factories;
using Source.Features.ScreenSize;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Source.Features.EntitySpawning.SpawningControllers
{
    public class PlayerSpawningController : IInitializable
    {
        private readonly PlayerEntityFactory _playerEntityFactory;
        private readonly PlayerEntityConfig _playerEntityConfig;
        private readonly FloorTileEntityConfig _floorTileEntityConfig;
        private readonly ScreenSizeController _screenSizeController;

        public PlayerSpawningController(
            PlayerEntityFactory playerEntityFactory,
            PlayerEntityConfig playerEntityConfig,
            FloorTileEntityConfig floorTileEntityConfig,
            ScreenSizeController screenSizeController)
        {
            _playerEntityFactory = playerEntityFactory;
            _playerEntityConfig = playerEntityConfig;
            _floorTileEntityConfig = floorTileEntityConfig;
            _screenSizeController = screenSizeController;
        }

        public void Initialize()
        {
            var floorTileSize = _floorTileEntityConfig
                .EntityMesh
                .bounds
                .size;

            // ToDO [ECS] Player and floor size need to be gotten differently
            var playerSize = _playerEntityConfig
                .PlayerEntityPrefab
                .GetComponentInChildren<MeshFilter>()
                .sharedMesh
                .bounds
                .size;
            
            var bottomLeftCorner = _screenSizeController.GetBottomLeftCorner();

            var spawnPosition = new float3(
                bottomLeftCorner.x + playerSize.x,
                bottomLeftCorner.y + floorTileSize.y + playerSize.y / 2,
                0);

            _playerEntityFactory.CreateEntityAt(spawnPosition);

            Blackboard.Set(BlackboardEntryId.PlayerSpawnPosition, spawnPosition);
        }
    }
}
