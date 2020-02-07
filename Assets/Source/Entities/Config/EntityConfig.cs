using Source.Features.EntitySpawning;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Entities.Config
{
    [CreateAssetMenu(fileName = nameof(EntityConfig), menuName = Constants.UMenuRoot + nameof(EntityConfig))]
    public class EntityConfig : ScriptableObject
    {
        [Serializable]
        public class EntitySetting
        {
            [SerializeField] private EntityType _entityType;
            public EntityType EntityType => _entityType;

            [SerializeField] private Mesh _entityMesh;
            public Mesh EntityMesh => _entityMesh;

            [SerializeField] private Material _entityMaterial;
            public Material EntityMaterial => _entityMaterial;

        }

        [SerializeField] private PlayerEntityConfig _playerEntityConfig;
        public PlayerEntityConfig PlayerEntityConfig => _playerEntityConfig;


        [Header("Entity Settings")]
        [SerializeField] private List<EntitySetting> _entitySettings;

        public EntitySetting GetEntitySetting(EntityType entityType)
        {
            return _entitySettings.First(setting => setting.EntityType == entityType);
        }
    }
}
