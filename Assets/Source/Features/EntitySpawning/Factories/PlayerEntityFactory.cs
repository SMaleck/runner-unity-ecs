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
                new BlobAssetStore().AddTo(Disposer)); // ToDo [ECS] Is this correct?

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
            SetPlayerComponentData(playerEntity, spawnPosition);

            var floorColliderEntity = EntityManager.Instantiate(_floorColliderEntityPrefab);
            SetFloorColliderComponentData(floorColliderEntity, playerEntity);

            return playerEntity;
        }

        private void SetPlayerComponentData(Entity entity, float3 position)
        {
            EntityManager.SetComponentData(entity, new Translation
            {
                Value = position
            });

            EntityManager.SetComponentData(entity, new MoveSpeed
            {
                Value = _playerEntityConfig.MoveSpeed
            });

            EntityManager.SetComponentData(entity, new MoveDirection
            {
                Value = _playerEntityConfig.MoveDirection
            });

            EntityManager.SetComponentData(entity, new JumpIntent
            {
                HasIntent = false,
                JumpForce = _playerEntityConfig.JumpForce
            });

            EntityManager.SetComponentData(entity, new TravelStats
            {
                Origin = position,
                CurrentPosition = position,
                DistanceTraveledUnits = 0
            });
        }

        private void SetFloorColliderComponentData(Entity floorColliderEntity, Entity playerEntity)
        {
            var playerPosition = EntityManager.GetComponentData<Translation>(playerEntity).Value;
            var playerHalfSize = GetPlayerSize(playerEntity) * 0.5f;
            var floorColliderOffset = GetFloorColliderOffset(floorColliderEntity, playerHalfSize);

            EntityManager.SetComponentData(floorColliderEntity, new Translation
            {
                Value = new float3(playerPosition.x, playerPosition.y - floorColliderOffset.y, playerPosition.z)
            });

            EntityManager.SetComponentData(floorColliderEntity, new FollowEntity
            {
                TargetEntity = playerEntity,
                Offset = floorColliderOffset,
                FollowX = true,
                FollowY = false
            });
        }

        private float3 GetPlayerSize(Entity entity)
        {
            var playerRenderMesh = EntityManager.GetSharedComponentData<RenderMesh>(entity);
            return playerRenderMesh.mesh.bounds.size;
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
