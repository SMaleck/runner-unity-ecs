using Unity.Entities;
using Unity.Mathematics;

namespace Source.Features.EntitySpawning.Factories
{
    public interface IEntityFactory
    {
        Entity CreateEntityAt(float3 spawnPosition);
    }
}
