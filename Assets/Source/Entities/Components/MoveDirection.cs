using Unity.Entities;
using Unity.Mathematics;

namespace Source.Entities.Components
{
    public struct MoveDirection : IComponentData
    {
        public float3 Value;
    }
}
