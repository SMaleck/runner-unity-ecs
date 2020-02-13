using Source.Entities.ComponentTags;
using Unity.Burst;
using Unity.Entities;

namespace Source.Entities.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(TagOutOfBoundsSystem))]
    public class DestroySystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity entity,
                ref DestroyTag destroyTag) =>
            {
                EntityManager.RemoveComponent(entity, typeof(DestroyTag));
                PostUpdateCommands.DestroyEntity(entity);
            });
        }
    }
}
