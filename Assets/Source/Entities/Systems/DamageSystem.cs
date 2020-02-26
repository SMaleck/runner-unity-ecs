using Source.Entities.Components;
using Source.Entities.ComponentTags;
using Source.Entities.Systems.Barriers;
using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;

namespace Source.Entities.Systems
{
    [BurstCompile]
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
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new SystemJob()
                .Schedule(this, inputDeps);
        }
    }
}
