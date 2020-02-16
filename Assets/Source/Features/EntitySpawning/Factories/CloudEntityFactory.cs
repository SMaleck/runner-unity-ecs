using Source.Features.EntitySpawning.Config;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Source.Features.EntitySpawning.Factories
{
    public class CloudEntityFactory : AbstractEntityFactory, IEntityFactory
    {
        private readonly CloudEntityConfig _cloudEntityConfig;
        private readonly List<Entity> _entityPrefabs;

        public CloudEntityFactory(CloudEntityConfig cloudEntityConfig)
        {
            _cloudEntityConfig = cloudEntityConfig;

            var conversionSettings = GameObjectConversionSettings.FromWorld(
                World.DefaultGameObjectInjectionWorld,
                new BlobAssetStore().AddTo(Disposer)); // ToDo [ECS] Is this correct?

            _entityPrefabs = _cloudEntityConfig.Prefabs
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
