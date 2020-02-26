using Source.Entities.Components;
using Source.Entities.ComponentTags;
using Source.Features.DataBridge;
using System;
using Source.Entities.Systems.Barriers;
using Unity.Entities;
using Unity.Jobs;

namespace Source.Entities.Systems
{
    [UpdateAfter(typeof(CollisionBarrierSystem))]
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
                damageIn.Value = 0;

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
