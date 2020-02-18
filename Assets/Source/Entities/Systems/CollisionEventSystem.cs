using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Source.Entities.Systems
{
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class CollisionEventSystem : JobComponentSystem
    {
        private BuildPhysicsWorld _buildPhysicsWorldSystem;
        private StepPhysicsWorld _stepPhysicsWorld;

        protected override void OnCreate()
        {
            _buildPhysicsWorldSystem = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        }

        struct SystemJob : ITriggerEventsJob
        {
            public void Execute(TriggerEvent triggerEvent)
            {
                UGF.Logger.Warn($"collision event: {triggerEvent}. Entities: {triggerEvent.Entities.EntityA}, {triggerEvent.Entities.EntityB}");
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var systemJob = new SystemJob();

            return systemJob.Schedule(
                _stepPhysicsWorld.Simulation,
                ref _buildPhysicsWorldSystem.PhysicsWorld,
                inputDependencies);
        }
    }
}
