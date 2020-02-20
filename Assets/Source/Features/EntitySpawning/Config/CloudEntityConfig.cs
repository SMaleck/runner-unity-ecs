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

        [Header("Spawning Range")]
        [SerializeField] private float _screenOffsetX;
        public float ScreenOffsetX => _screenOffsetX;

        [SerializeField] private float _positionRangeMinY;
        public float PositionRangeMinY => _positionRangeMinY;

        [SerializeField] private float _positionRangeMaxY;
        public float PositionRangeMaxY => _positionRangeMaxY;
    }
}
