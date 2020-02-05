using Source.Entities.Components.Tags;
using Source.Features.DataBridge;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Source.Entities.Systems
{
    public class PositionTrackingSystem : JobComponentSystem
    {
        [RequireComponentTag(typeof(PlayerTag))]
        private struct PositionTrackingJob : IJobForEach<Translation>
        {
            public float3 Origin;

            public void Execute([ReadOnly] ref Translation translation)
            {
                Blackboard.Set(BlackboardEntryId.PlayerPosition, translation.Value);

                var distanceTraveled = math.abs(translation.Value.x - Origin.x);
                Blackboard.Set(BlackboardEntryId.PlayerDistanceTraveled, distanceTraveled);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            Blackboard.TryGet(BlackboardEntryId.PlayerSpawnPosition, out float3 origin);
            var job = new PositionTrackingJob()
            {
                Origin = origin
            };

            return job.Schedule(this, inputDeps);
        }
    }
}
