using Unity.Entities;

namespace Source.Entities.ComponentTags
{
    public struct PlayerTag : IComponentData
    {
    }

    public class PlayerTagComponent : ComponentDataProxy<PlayerTag>
    {
    }
}
