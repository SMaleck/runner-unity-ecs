using UnityEngine;

namespace Source.Features.EntitySpawning
{
    public interface IObstacleSpawner
    {
        void SpawnObstacleAt(Vector3 position);
    }
}
