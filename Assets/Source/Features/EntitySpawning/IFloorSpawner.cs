using UnityEngine;

namespace Source.Features.EntitySpawning
{
    public interface IFloorSpawner
    {
        void SpawnFloorTileAt(Vector3 position);
    }
}
