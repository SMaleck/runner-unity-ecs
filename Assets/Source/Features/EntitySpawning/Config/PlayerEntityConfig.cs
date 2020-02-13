using System;
using Source.Features.EntitySpawning.Converters;
using UnityEngine;

namespace Source.Features.EntitySpawning.Config
{
    [Serializable]
    public class PlayerEntityConfig
    {
        [Header("Prefabs")]
        [SerializeField] private PlayerEntity _playerEntityPrefab;
        public GameObject PlayerEntityPrefab => _playerEntityPrefab.gameObject;

        [SerializeField] private FloorColliderEntity _floorColliderPrefab;
        public GameObject FloorColliderPrefab => _floorColliderPrefab.gameObject;

        [Header("Player Attributes")]
        [SerializeField] private Vector3 _moveDirection;
        public Vector3 MoveDirection => _moveDirection;

        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private float _jumpForce;
        public float JumpForce => _jumpForce;

        [SerializeField] private int _health;
        public int Health => _health;
    }
}
