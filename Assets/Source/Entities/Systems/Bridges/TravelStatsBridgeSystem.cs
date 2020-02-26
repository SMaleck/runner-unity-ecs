using Source.Entities.Components;
using Source.Entities.ComponentTags;
using Source.Features.DataBridge;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Source.Entities.Systems.Bridges
{
    [UpdateAfter(typeof(TravelStatsSystem))]
    public class TravelStatsBridgeSystem : JobComponentSystem
    {
        [RequireComponentTag(typeof(PlayerTag))]
        private struct SystemJob : IJobForEach<TravelStats>
        {
            public void Execute([ReadOnly] ref TravelStats travelStats)
            {
                Blackboard.Set(BlackboardEntryId.PlayerPosition, travelStats.CurrentPosition);
                Blackboard.Set(BlackboardEntryId.PlayerDistanceTraveled, travelStats.DistanceTraveledUnits);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new SystemJob()
                .Schedule(this, inputDeps);
        }
    }
}
