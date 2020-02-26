using Source.Entities.Components;
using Source.Entities.ComponentTags;
using Source.Features.DataBridge;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Source.Entities.Systems.Bridges
{
    [UpdateAfter(typeof(DamageSystem))]
    public class HealthBridgeSystem : JobComponentSystem
    {
        [RequireComponentTag(typeof(PlayerTag))]
        private struct SystemJob : IJobForEach<Health>
        {
            public void Execute([ReadOnly] ref Health health)
            {
                Blackboard.Set(BlackboardEntryId.PlayerHealth, health.Value);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new SystemJob()
                .Schedule(this, inputDeps);
        }
    }
}
