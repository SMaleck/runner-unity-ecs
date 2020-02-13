using Unity.Entities;

namespace Source.Entities.ComponentTags
{
    public struct InputDriverTag : IComponentData
    {
    }

    public class InputDriverTagComponent : ComponentDataProxy<InputDriverTag>
    {
    }
}
