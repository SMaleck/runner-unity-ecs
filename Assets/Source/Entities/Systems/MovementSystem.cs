using Source.Entities.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace Source.Entities.Systems
{
    [BurstCompile]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class MovementSystem : JobComponentSystem
    {
        [BurstCompile]
        struct MovementSystemJob : IJobForEach<Translation, MoveSpeed, MoveDirection>
        {
            public float DeltaTime;

            public void Execute(
                ref Translation translation,
                [ReadOnly] ref MoveSpeed moveSpeed,
                [ReadOnly] ref MoveDirection moveDirection)
            {
                translation.Value += DeltaTime * moveSpeed.Value * moveDirection.Value;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var moveForwardRotationJob = new MovementSystemJob
            {
                DeltaTime = Time.DeltaTime
            };

            return moveForwardRotationJob.Schedule(this, inputDeps);
        }
    }
}
