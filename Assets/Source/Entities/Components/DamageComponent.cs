using Unity.Entities;

namespace Source.Entities.Components
{
    public struct Damage : IComponentData
    {
        public int Value;
    }

    public class DamageComponent : ComponentDataProxy<Damage>
    {
    }
}
