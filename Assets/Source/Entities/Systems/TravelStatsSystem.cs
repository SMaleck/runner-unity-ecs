using Source.Entities.Components;
using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace Source.Entities.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(BuildPhysicsWorld))]
    public class TravelStatsSystem : JobComponentSystem
    {
        [BurstCompile]
        private struct TravelStatsSystemJob : IJobForEach<Translation, TravelStats>
        {
            public void Execute(
                [ReadOnly] ref Translation translation,
                ref TravelStats travelStats)
            {
                travelStats.CurrentPosition = translation.Value;
                travelStats.DistanceTraveledUnits = Math.Abs(travelStats.Origin.x - translation.Value.x);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new TravelStatsSystemJob();
            return job.Schedule(this, inputDeps);
        }
    }
}
