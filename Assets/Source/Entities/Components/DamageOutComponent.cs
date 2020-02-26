using Unity.Entities;

namespace Source.Entities.Components
{
    public struct DamageOut : IComponentData
    {
        public int Value;
    }

    public class DamageOutComponent : ComponentDataProxy<DamageOut>
    {
    }
}
