using Source.Entities.Components;
using Source.Entities.ComponentTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Source.Entities.Systems
{
    [BurstCompile]
    public class DeathSystem : JobComponentSystem
    {
        [RequireComponentTag(typeof(PlayerTag))]
        private struct SystemJob : IJobForEach<Health>
        {
            public void Execute([ReadOnly] ref Health health)
            {
                if (health.Value <= 0)
                {
                    // ToDo [ECS] Communicate death back to Mono Layer
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new SystemJob();
            return job.Schedule(this, inputDeps);
        }
    }
}
