using Unity.Entities;

namespace Source.Entities.Components.Tags
{
    public struct PlayerTag : IComponentData
    {
    }

    public class PlayerTagComponent : ComponentDataProxy<PlayerTag>
    {
    }
}
