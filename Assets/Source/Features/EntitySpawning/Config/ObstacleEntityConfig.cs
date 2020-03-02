using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Features.EntitySpawning.Config
{
    [Serializable]
    public class ObstacleEntityConfig
    {
        [SerializeField] private float _spawnPositionY;
        public float SpawnPositionY => _spawnPositionY;

        [SerializeField] private List<GameObject> _prefabs;
        public List<GameObject> Prefabs => _prefabs;
    }
}
