using Source.Features.EntitySpawning.Config;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Source.Features.EntitySpawning.Factories
{
    public class ObstacleEntityFactory : AbstractEntityFactory, IEntityFactory
    {
        private readonly ObstacleEntityConfig _obstacleEntityConfig;
        private readonly List<Entity> _entityPrefabs;

        public ObstacleEntityFactory(ObstacleEntityConfig obstacleEntityConfig)
        {
            _obstacleEntityConfig = obstacleEntityConfig;

            var conversionSettings = GameObjectConversionSettings.FromWorld(
                World.DefaultGameObjectInjectionWorld,
                new BlobAssetStore().AddTo(Disposer)); // ToDo [ECS] Is this correct?

            _entityPrefabs = _obstacleEntityConfig.Prefabs
                .Select(prefab => GameObjectConversionUtility.ConvertGameObjectHierarchy(
                    prefab,
                    conversionSettings))
                .ToList();
        }

        public Entity CreateEntityAt(float3 spawnPosition)
        {
            var entityPrefab = GetRandomEntityPrefab();
            var entity = EntityManager.Instantiate(entityPrefab);

            EntityManager.SetComponentData(entity, new Translation
            {
                Value = spawnPosition
            });

            return entity;
        }

        private Entity GetRandomEntityPrefab()
        {
            var randomIndex = UnityEngine.Random.Range(0, _entityPrefabs.Count);
            return _entityPrefabs[randomIndex];
        }
    }
}
