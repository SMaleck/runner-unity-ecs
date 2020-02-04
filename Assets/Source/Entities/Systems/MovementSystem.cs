using Source.Entities.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Source.Entities.Systems
{
    public class MovementSystem : JobComponentSystem
    {
        [BurstCompile]
        struct MovementSystemJob : IJobForEach<Translation, Rotation, MoveSpeed>
        {
            public float DeltaTime;

            public void Execute(
                ref Translation translation, 
                [ReadOnly] ref Rotation rotation, 
                [ReadOnly] ref MoveSpeed moveSpeed)
            {
                translation.Value += DeltaTime * moveSpeed.Value * math.forward(rotation.Value);
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
