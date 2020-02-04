using Source.Entities.Components.Tags;
using Source.Features.DataBridge;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Source.Entities.Systems
{
    public class PositionTrackingSystem : JobComponentSystem
    {
        [RequireComponentTag(typeof(PlayerTag))]
        private struct PositionTrackingJob : IJobForEach<Translation>
        {
            public void Execute([ReadOnly] ref Translation translation)
            {
                Blackboard.Set(BlackboardEntryId.PlayerPosition, translation.Value);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new PositionTrackingJob();

            return job.Schedule(this, inputDeps);
        }
    }
}
