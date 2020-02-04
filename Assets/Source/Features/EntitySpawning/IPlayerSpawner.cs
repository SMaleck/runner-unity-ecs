using UnityEngine;

namespace Assets.Source.Features.EntitySpawning
{
    public interface IPlayerSpawner
    {
        void SpawnPlayerAt(Vector3 position);
    }
}
