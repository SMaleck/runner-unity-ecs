using Unity.Entities;
using Unity.Mathematics;

namespace Source.Entities.Components
{
    public struct PlayerTagFollow : IComponentData
    {
        public float3 Offset;
        public bool FollowX;
        public bool FollowY;
    }
}
