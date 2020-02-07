using System;
using UnityEngine;

namespace Source.Entities.Config
{
    [Serializable]
    public class ConvertedEntityConfig
    {
        [SerializeField] private GameObject _prefab;
        public GameObject Prefab => _prefab;
    }
}
