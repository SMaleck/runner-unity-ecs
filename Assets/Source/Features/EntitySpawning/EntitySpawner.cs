using Assets.Source.Features.EntitySpawning;
using Source.Entities.Config;
using System.Collections.Generic;
using UGF.Util.UniRx;
using Unity.Entities;
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
                typeof(RenderMesh));
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

            EntityManager.Instantiate(entity);
        }

        public void SpawnFloorTileAt(Vector3 position)
        {
            var entity = CreateEntityAt(EntityType.Floor, position);

            EntityManager.Instantiate(entity);
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
