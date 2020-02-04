using System;
using Source.Entities.Config;
using UGF.Util.UniRx;
using UniRx;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Zenject;

namespace Source.Features.EntitySpawning
{
    public class EntitySpawner : AbstractDisposable,  IInitializable
    {
        private readonly EntityConfig _entityConfig;
        private EntityManager EntityManager => World.DefaultGameObjectInjectionWorld.EntityManager;

        private readonly EntityArchetype _playerArchetype;
        private readonly EntityArchetype _floorArchetype;
        private readonly EntityArchetype _obstacleArchetype;

        public EntitySpawner(EntityConfig entityConfig)
        {
            _entityConfig = entityConfig;

            _playerArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh));

            _floorArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh));

            _obstacleArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation),
                typeof(RenderMesh));
        }

        public void Initialize()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (Input.GetKeyDown("q"))
                    {
                        SpawnEntityAt(EntityType.Floor, Vector3.zero);
                    }
                })
                .AddTo(Disposer);

        }

        public void SpawnEntityAt(EntityType entityType, Vector3 position)
        {
            switch (entityType)
            {
                case EntityType.Player:
                    break;
                case EntityType.Floor:
                    SpawnFloorAt(position);
                    break;
                case EntityType.Obstacle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }
        }

        private void SpawnFloorAt(Vector3 position)
        {
            var entity = EntityManager.CreateEntity(_floorArchetype);

            EntityManager.SetComponentData(entity, new Translation { Value = position });
            EntityManager.SetSharedComponentData(entity, CreateRenderMeshFor(EntityType.Floor));

            EntityManager.Instantiate(entity);
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
