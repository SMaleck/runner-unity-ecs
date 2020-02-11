using Source.Entities.Components;
using Source.Entities.Config;
using Source.Features.ScreenSize;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Source.Features.EntitySpawning.Factories
{
    public class FloorTileEntityFactory : AbstractEntityFactory, IEntityFactory
    {
        private const EntityType EntityFactoryType = EntityType.Floor;

        private readonly EntityConfig _entityConfig;
        private readonly ScreenSizeModel _screenSizeModel;
        private readonly EntityArchetype _entityArchetype;

        public FloorTileEntityFactory(
            EntityConfig entityConfig,
            ScreenSizeModel screenSizeModel)
        {
            _entityConfig = entityConfig;
            _screenSizeModel = screenSizeModel;
            _entityArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh),
                typeof(KillThresholdX));
        }

        public Entity CreateEntityAt(float3 spawnPosition)
        {
            var entity = EntityManager.CreateEntity(_entityArchetype);

            EntityManager.SetComponentData(entity, new Translation
            {
                Value = spawnPosition
            });

            EntityManager.SetComponentData(entity, new KillThresholdX
            {
                Value = 0.5f + _screenSizeModel.WidthExtendUnits
            });

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
