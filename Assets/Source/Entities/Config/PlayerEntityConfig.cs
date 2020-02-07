using Source.Entities.EntityConverters;
using System;
using UnityEngine;

namespace Source.Entities.Config
{
    [Serializable]
    public class PlayerEntityConfig
    {
        [SerializeField] private PlayerEntity _playerEntityPrefab;
        public GameObject PlayerEntityPrefab => _playerEntityPrefab.gameObject;

        [SerializeField] private FloorColliderEntity _floorColliderPrefab;
        public GameObject FloorColliderPrefab => _floorColliderPrefab.gameObject;
    }
}
