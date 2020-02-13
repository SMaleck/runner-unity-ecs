using Source.Entities.Components;
using Source.Features.EntitySpawning.Config;
using Source.Features.ScreenSize;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Source.Features.EntitySpawning.Factories
{
    public class FloorTileEntityFactory : AbstractEntityFactory, IEntityFactory
    {
        private readonly FloorTileEntityConfig _floorTileEntityConfig;
        private readonly ScreenSizeModel _screenSizeModel;
        private readonly EntityArchetype _entityArchetype;

        public FloorTileEntityFactory(
            FloorTileEntityConfig floorTileEntityConfig,
            ScreenSizeModel screenSizeModel)
        {
            _floorTileEntityConfig = floorTileEntityConfig;
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

            EntityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = _floorTileEntityConfig.EntityMesh,
                material = _floorTileEntityConfig.EntityMaterial
            });

            return entity;
        }
    }
}
