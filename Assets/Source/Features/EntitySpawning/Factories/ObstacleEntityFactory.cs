using Source.Entities.Components;
using Source.Entities.Config;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Source.Features.EntitySpawning.Factories
{
    public class ObstacleEntityFactory : AbstractEntityFactory, IEntityFactory
    {
        private const EntityType EntityFactoryType = EntityType.Obstacle;

        private readonly EntityConfig _entityConfig;
        private readonly EntityArchetype _entityArchetype;

        public ObstacleEntityFactory(EntityConfig entityConfig)
        {
            _entityConfig = entityConfig;
            _entityArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh),
                typeof(FollowEntity));
        }

        public Entity CreateEntityAt(float3 spawnPosition)
        {
            var entity = EntityManager.CreateEntity(_entityArchetype);

            EntityManager.SetComponentData(entity, new Translation { Value = spawnPosition });
            EntityManager.SetSharedComponentData(entity, CreateRenderMeshFor(EntityFactoryType));

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
