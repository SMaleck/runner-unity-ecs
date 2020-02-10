using Unity.Entities;
using Unity.Mathematics;

namespace Source.Entities.Components
{
    public struct TravelStats : IComponentData
    {
        public float3 Origin;
        public float3 CurrentPosition;
        public float DistanceTraveledUnits;
    }
}
