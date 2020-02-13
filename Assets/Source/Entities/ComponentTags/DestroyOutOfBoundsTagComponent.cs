using Unity.Entities;

namespace Source.Entities.ComponentTags
{
    public struct DestroyOutOfBoundsTag : IComponentData
    {
    }

    public class DestroyOutOfBoundsTagComponent : ComponentDataProxy<DestroyOutOfBoundsTag>
    {
    }
}
