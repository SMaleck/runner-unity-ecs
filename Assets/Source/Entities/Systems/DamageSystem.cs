using Source.Entities.Components;
using Source.Entities.ComponentTags;
using Source.Features.DataBridge;
using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Source.Entities.Systems
{
    public class DamageSystem : JobComponentSystem
    {
        [RequireComponentTag(typeof(PlayerTag))]
        private struct SystemJob : IJobForEach<DamageIn, Health>
        {
            public void Execute(
                ref DamageIn damageIn,
                ref Health health)
            {
                health.Value = Math.Max(0, health.Value - damageIn.Value);

                Blackboard.Set(BlackboardEntryId.PlayerHealth, health.Value);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new SystemJob();
            return job.Schedule(this, inputDeps);
        }
    }
}
