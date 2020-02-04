using Source.Entities.Config;
using System.Collections.Generic;
using Source.Entities.Components;
using UGF.Util.UniRx;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Source.Features.EntitySpawning
{
    public class EntitySpawner : AbstractDisposable, IPlayerSpawner, IFloorSpawner
    {
        private readonly EntityConfig _entityConfig;
        private readonly Dictionary<EntityType, EntityArchetype> _entityArchetypes;

        private EntityManager EntityManager => World.DefaultGameObjectInjectionWorld.EntityManager;

        public EntitySpawner(EntityConfig entityConfig)
        {
            _entityConfig = entityConfig;
            _entityArchetypes = new Dictionary<EntityType, EntityArchetype>();

            var playerArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh),
                typeof(MoveSpeed),
                typeof(MoveDirection));
            _entityArchetypes.Add(EntityType.Player, playerArchetype);

            var floorArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh));
            _entityArchetypes.Add(EntityType.Floor, floorArchetype);

            var obstacleArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh));
            _entityArchetypes.Add(EntityType.Obstacle, obstacleArchetype);
        }

        public void SpawnPlayerAt(Vector3 position)
        {
            var entity = CreateEntityAt(EntityType.Player, position);

            EntityManager.SetComponentData(entity, new MoveSpeed{ Value = 2 });
            EntityManager.SetComponentData(entity, new MoveDirection { Value = new float3(1, 0, 0) });
        }

        public void SpawnFloorTileAt(Vector3 position)
        {
            CreateEntityAt(EntityType.Floor, position);
        }

        public void SpawnFloorTilesAt(Vector3[] positions)
        {
            var entities = CreateEntitiesAt(EntityType.Floor, positions);
            entities.Dispose();
        }

        private Entity CreateEntityAt(
            EntityType entityType,
            Vector3 position)
        {
            var archetype = _entityArchetypes[entityType];
            var entity = EntityManager.CreateEntity(archetype);

            EntityManager.SetComponentData(entity, new Translation { Value = position });
            EntityManager.SetSharedComponentData(entity, CreateRenderMeshFor(entityType));

            return entity;
        }

        private NativeArray<Entity> CreateEntitiesAt(
            EntityType entityType,
            Vector3[] positions)
        {
            var archetype = _entityArchetypes[entityType];
            var entities = new NativeArray<Entity>(positions.Length, Allocator.Temp);
            EntityManager.CreateEntity(archetype, entities);

            for (var i = 0; i < positions.Length; i++)
            {
                EntityManager.SetComponentData(entities[i], new Translation { Value = positions[i] });
                EntityManager.SetSharedComponentData(entities[i], CreateRenderMeshFor(entityType));
            }

            return entities;
        }

        private RenderMesh CreateRenderMeshFor(EntityType entityType)
        {
            var entitySetting = _entityConfig.GetEntitySetting(entityType);

            return new RenderMesh
            {
                mesh = entitySetting.EntityMesh,
                material = entitySetting.EntityMaterial
            };
        }
    }
}
