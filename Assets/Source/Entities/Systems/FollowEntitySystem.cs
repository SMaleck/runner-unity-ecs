using Source.Entities.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Source.Entities.Systems
{
    public class FollowEntitySystem : JobComponentSystem
    {
        struct FollowEntityJob : IJobForEach<Translation, FollowEntity>
        {
            [ReadOnly] public ComponentDataFromEntity<LocalToWorld> LocalToWorldFromTargetEntity;

            public void Execute(ref Translation translation, [ReadOnly] ref FollowEntity followEntity)
            {
                if (!LocalToWorldFromTargetEntity.Exists(followEntity.TargetEntity))
                {
                    return;
                }

                var targetPosition = LocalToWorldFromTargetEntity[followEntity.TargetEntity].Position;

                var nextX = followEntity.FollowX
                    ? (targetPosition.x + followEntity.Offset.x)
                    : translation.Value.x;

                var nextY = followEntity.FollowY
                    ? (targetPosition.y + followEntity.Offset.y)
                    : translation.Value.y;

                translation.Value = new float3(nextX, nextY, translation.Value.z);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            FollowEntityJob job = new FollowEntityJob()
            {
                LocalToWorldFromTargetEntity = GetComponentDataFromEntity<LocalToWorld>(true)
            };

            return job.Schedule(this, inputDeps);
        }
    }
}
