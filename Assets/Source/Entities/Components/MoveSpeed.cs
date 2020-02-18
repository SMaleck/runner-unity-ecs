using System;
using Unity.Entities;

namespace Source.Entities.Components
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public float SpeedPerSecond;
    }
}
