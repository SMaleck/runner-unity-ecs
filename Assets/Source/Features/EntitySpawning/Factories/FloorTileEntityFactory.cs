using Source.Entities.ComponentTags;
using Source.Features.EntitySpawning.Config;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Source.Features.EntitySpawning.Factories
{
    /// <summary>
    /// Referenced in ReadMe under: 3 - Pure code
    /// </summary>
    public class FloorTileEntityFactory : AbstractEntityFactory, IEntityFactory
    {
        private readonly FloorTileEntityConfig _floorTileEntityConfig;
        private readonly EntityArchetype _entityArchetype;

        public FloorTileEntityFactory(FloorTileEntityConfig floorTileEntityConfig)
        {
            _floorTileEntityConfig = floorTileEntityConfig;
            _entityArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh),
                typeof(DestroyOutOfBoundsTag));
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
                mesh = _floorTileEntityConfig.EntityMesh,
                material = _floorTileEntityConfig.EntityMaterial
            });

            return entity;
        }
    }
}
