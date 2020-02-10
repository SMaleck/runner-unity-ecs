using Source.Entities.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace Source.Entities.Systems
{
    [BurstCompile]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class JumpSystem : JobComponentSystem
    {
        private struct JumpSystemJob : IJobForEach<JumpIntent, Translation, Rotation, PhysicsVelocity, PhysicsMass>
        {
            public void Execute(
                ref JumpIntent jumpIntent,
                [ReadOnly] ref Translation translation,
                [ReadOnly] ref Rotation rotation,
                ref PhysicsVelocity physicsVelocity,
                [ReadOnly] ref PhysicsMass physicsMass)
            {
                if (!jumpIntent.HasIntent)
                {
                    return;
                }

                jumpIntent.HasIntent = false;

                physicsVelocity.ApplyImpulse(
                    physicsMass,
                    translation,
                    rotation,
                    new float3(0, jumpIntent.JumpForce, 0),
                    translation.Value);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new JumpSystemJob();
            return job.Schedule(this, inputDeps);
        }
    }
}
