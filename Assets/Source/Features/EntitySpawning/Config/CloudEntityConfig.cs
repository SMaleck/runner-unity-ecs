using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Features.EntitySpawning.Config
{
    [Serializable]
    public class CloudEntityConfig
    {
        [SerializeField] private List<GameObject> _prefabs;
        public List<GameObject> Prefabs => _prefabs;
    }
}
