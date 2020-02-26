using Source.Entities.Components;
using Source.Entities.ComponentTags;
using Unity.Burst;
using Unity.Entities;

namespace Source.Entities.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(DamageSystem))]
    public class DeathSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity entity,
                ref PlayerTag playerTag,
                ref Health health,
                ref InputDriverTag inputDriverTag) =>
            {
                if (health.Value > 0)
                {
                    return;
                }

                EntityManager.RemoveComponent(entity, typeof(MoveSpeed));
                EntityManager.RemoveComponent(entity, typeof(InputDriverTag));
            });
        }
    }
}
