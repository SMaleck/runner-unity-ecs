using Unity.Entities;

namespace Source.Entities.Components
{
    public struct JumpIntent : IComponentData
    {
        public bool HasIntent;
        public float JumpForce;
    }
}
