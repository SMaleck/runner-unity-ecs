using System;
using UnityEngine;

namespace Source.Features.EntitySpawning.Config
{
    [Serializable]
    public class FloorTileEntityConfig
    {
        [SerializeField] private Mesh _entityMesh;
        public Mesh EntityMesh => _entityMesh;

        [SerializeField] private Material _entityMaterial;
        public Material EntityMaterial => _entityMaterial;
    }
}
