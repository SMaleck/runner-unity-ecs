using Source.Entities.Components;
using Source.Features.DataBridge;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Source.Entities.Systems
{
    public class FollowPlayerTagSystem : JobComponentSystem
    {
        private struct FollowPlayerTagJob : IJobForEach<PlayerTagFollow, Translation>
        {
            public void Execute(
                [ReadOnly] ref PlayerTagFollow playerTagFollow,
                ref Translation translation)
            {
                if (!Blackboard.TryGet(BlackboardEntryId.PlayerPosition, out float3 playerPosition))
                {
                    return;
                }

                var nextX = playerTagFollow.FollowX
                    ? (playerPosition.x + playerTagFollow.Offset.x)
                    : translation.Value.x;

                var nextY = playerTagFollow.FollowY
                    ? (playerPosition.y + playerTagFollow.Offset.y)
                    : translation.Value.y;

                translation.Value = new float3(nextX, nextY, translation.Value.z);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new FollowPlayerTagJob();

            return job.Schedule(this, inputDeps);
        }
    }
}
