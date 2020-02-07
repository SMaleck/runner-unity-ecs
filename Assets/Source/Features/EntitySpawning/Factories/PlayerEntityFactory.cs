using Source.Entities.Components;
using Source.Entities.Config;
using UniRx;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Source.Features.EntitySpawning.Factories
{
    public class PlayerEntityFactory : AbstractEntityFactory, IEntityFactory
    {
        private readonly PlayerEntityConfig _playerEntityConfig;
        private readonly Entity _playerEntityPrefab;
        private readonly Entity _floorColliderEntityPrefab;

        public PlayerEntityFactory(PlayerEntityConfig playerEntityConfig)
        {
            _playerEntityConfig = playerEntityConfig;

            var settings = GameObjectConversionSettings.FromWorld(
                World.DefaultGameObjectInjectionWorld,
                new BlobAssetStore().AddTo(Disposer));

            _playerEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(
                _playerEntityConfig.PlayerEntityPrefab,
                settings);

            _floorColliderEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(
                _playerEntityConfig.FloorColliderPrefab,
                settings);
        }

        public Entity CreateEntityAt(float3 spawnPosition)
        {
            var playerEntity = EntityManager.Instantiate(_playerEntityPrefab);

            EntityManager.SetComponentData(playerEntity, new Translation { Value = spawnPosition });
            EntityManager.SetComponentData(playerEntity, new MoveSpeed { Value = 2 });
            EntityManager.SetComponentData(playerEntity, new MoveDirection { Value = new float3(1, 0, 0) });

            var playerRenderMesh = EntityManager.GetSharedComponentData<RenderMesh>(playerEntity);
            var playerHalfSize = playerRenderMesh.mesh.bounds.size * 0.5f;

            var floorColliderEntity = EntityManager.Instantiate(_floorColliderEntityPrefab);
            var floorColliderOffset = GetFloorColliderOffset(floorColliderEntity, playerHalfSize);

            EntityManager.SetComponentData(floorColliderEntity, new Translation
            {
                Value = new float3(spawnPosition.x, spawnPosition.y - floorColliderOffset.y, spawnPosition.z)
            });

            EntityManager.SetComponentData(floorColliderEntity, new FollowEntity
            {
                TargetEntity = playerEntity,
                Offset = floorColliderOffset,
                FollowX = true,
                FollowY = false
            });

            return playerEntity;
        }

        private float3 GetFloorColliderOffset(Entity floorColliderEntity, float3 playerHalfSize)
        {
            // ToDo Get correct collider size for floor
            //var floorCollider = EntityManager.GetComponentData<PhysicsCollider>(floorColliderEntity);
            //var floorHalfSize = floorCollider.ColliderPtr

            return new float3(
                0,
                playerHalfSize.y + 0.5f,
                0);
        }
    }
}
