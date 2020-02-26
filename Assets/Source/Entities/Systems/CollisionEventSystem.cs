using Source.Entities.Components;
using Source.Entities.Systems.Barriers;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Source.Entities.Systems
{
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    [UpdateBefore(typeof(CollisionBarrierSystem))]
    public class CollisionEventSystem : JobComponentSystem
    {
        private BuildPhysicsWorld _buildPhysicsWorldSystem;
        private StepPhysicsWorld _stepPhysicsWorld;
        private CollisionBarrierSystem _collisionBarrierSystem;

        protected override void OnCreate()
        {
            _buildPhysicsWorldSystem = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            _collisionBarrierSystem = World.GetOrCreateSystem<CollisionBarrierSystem>();
        }

        struct SystemJob : ITriggerEventsJob
        {
            public EntityCommandBuffer.Concurrent CommandBuffer;
            public ComponentDataFromEntity<DamageIn> DamageReceivers;
            public ComponentDataFromEntity<DamageOut> DamageDealers;

            public void Execute(TriggerEvent triggerEvent)
            {
                if (!TryGetEntity(triggerEvent, DamageReceivers, out Entity damageReceivingEntity, out DamageIn damageIn))
                {
                    return;
                }

                if (!TryGetEntity(triggerEvent, DamageDealers, out Entity damageDealingEntity, out DamageOut damageOut))
                {
                    return;
                }

                CommandBuffer.RemoveComponent(
                    damageDealingEntity.Index,
                    damageDealingEntity,
                    typeof(DamageOut));

                CommandBuffer.SetComponent(
                    damageReceivingEntity.Index,
                    damageReceivingEntity,
                    new DamageIn
                    {
                        Value = damageIn.Value + damageOut.Value
                    });
            }

            private bool TryGetEntity<T>(
                TriggerEvent triggerEvent,
                ComponentDataFromEntity<T> componentData,
                out Entity entity,
                out T component) where T : struct, IComponentData
            {
                entity = default;
                component = default;

                if (componentData.Exists(triggerEvent.Entities.EntityA))
                {
                    entity = triggerEvent.Entities.EntityA;
                    component = componentData[entity];
                    return true;
                }

                if (componentData.Exists(triggerEvent.Entities.EntityB))
                {
                    entity = triggerEvent.Entities.EntityB;
                    component = componentData[entity];
                    return true;
                }

                return false;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var damageReceivers = GetComponentDataFromEntity<DamageIn>();
            var damageDealers = GetComponentDataFromEntity<DamageOut>();

            var systemJob = new SystemJob()
            {
                CommandBuffer = _collisionBarrierSystem.CreateCommandBuffer().ToConcurrent(),
                DamageReceivers = damageReceivers,
                DamageDealers = damageDealers
            };

            inputDependencies = systemJob.Schedule(
                _stepPhysicsWorld.Simulation,
                ref _buildPhysicsWorldSystem.PhysicsWorld,
                inputDependencies);

            _collisionBarrierSystem.AddJobHandleForProducer(inputDependencies);

            return inputDependencies;
        }
    }
}
