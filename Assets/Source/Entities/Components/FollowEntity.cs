using Unity.Entities;
using Unity.Mathematics;

namespace Source.Entities.Components
{
    public struct FollowEntity : IComponentData
    {
        public Entity TargetEntity;

        public float3 Offset;
        public bool FollowX;
        public bool FollowY;
    }
}
