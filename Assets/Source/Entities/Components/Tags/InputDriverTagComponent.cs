using Unity.Entities;

namespace Source.Entities.Components.Tags
{
    public struct InputDriverTag : IComponentData
    {
    }

    public class InputDriverTagComponent : ComponentDataProxy<InputDriverTag>
    {
    }
}
