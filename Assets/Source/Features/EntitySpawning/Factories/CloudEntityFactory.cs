using Source.Entities.Components;
using Source.Features.EntitySpawning.Config;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Source.Features.EntitySpawning.Factories
{
    public class CloudEntityFactory : AbstractEntityFactory, IEntityFactory
    {
        private readonly CloudEntityConfig _cloudEntityConfig;
        private readonly EntityArchetype _entityArchetype;

        public CloudEntityFactory(CloudEntityConfig cloudEntityConfig)
        {
            _cloudEntityConfig = cloudEntityConfig;

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

            EntityManager.SetComponentData(entity, new Translation
            {
                Value = spawnPosition
            });

            EntityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = _cloudEntityConfig.EntityMesh,
                material = _cloudEntityConfig.EntityMaterial
            });

            return entity;
        }
    }
}
