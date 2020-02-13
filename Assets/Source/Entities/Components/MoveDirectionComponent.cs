using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Source.Entities.Components
{
    [Serializable]
    public struct MoveDirection : IComponentData
    {
        public float3 Direction;
    }

    public class MoveDirectionComponent : ComponentDataProxy<MoveDirection>
    {
    }
}
