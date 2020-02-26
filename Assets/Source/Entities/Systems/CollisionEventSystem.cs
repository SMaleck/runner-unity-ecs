using Source.Entities.Components;
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
                if (!TryGetComponent(triggerEvent, out Entity damageReceivingEntity, out DamageIn damageIn))
                {
                    return;
                }

                if (!TryGetComponent(triggerEvent, out Entity damageDealingEntity, out DamageOut damageOut))
                {
                    return;
                }

                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                entityManager.RemoveComponent<DamageOut>(damageDealingEntity);
                
                entityManager.SetComponentData(damageReceivingEntity, new DamageIn
                {
                    Value = damageIn.Value + damageOut.Value
                });
            }

            private bool TryGetComponent<T>(
                TriggerEvent triggerEvent,
                out Entity entity,
                out T componentData) where T : struct, IComponentData
            {
                entity = default;
                componentData = default;

                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                if (entityManager.HasComponent<T>(triggerEvent.Entities.EntityA))
                {
                    entity = triggerEvent.Entities.EntityA;
                    componentData = entityManager.GetComponentData<T>(triggerEvent.Entities.EntityA);
                    return true;
                }
                if (entityManager.HasComponent<T>(triggerEvent.Entities.EntityB))
                {
                    entity = triggerEvent.Entities.EntityB;
                    componentData = entityManager.GetComponentData<T>(triggerEvent.Entities.EntityB);
                    return true;
                }

                return false;
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
