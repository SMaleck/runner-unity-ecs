using Source.Entities.ComponentTags;
using Source.Entities.Systems.Bridges;
using Source.Features.DataBridge;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Systems;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Source.Entities.Systems
{
    [UpdateAfter(typeof(BuildPhysicsWorld))]
    [UpdateAfter(typeof(TravelStatsBridgeSystem))]
    public class TagOutOfBoundsSystem : ComponentSystem
    {
        private EntityQuery _entityQuery; // ToDo [ECS] How could this query be used?

        protected override void OnCreate()
        {
            var query = new EntityQueryDesc
            {
                None = new ComponentType[] { typeof(DestroyTag) },
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Translation>(),
                    ComponentType.ReadOnly<DestroyOutOfBoundsTag>()
                }
            };

            _entityQuery = GetEntityQuery(query);
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity entity,
                ref Translation translation,
                ref DestroyOutOfBoundsTag destroyOutOfBoundsTag) =>
            {
                if (EntityManager.HasComponent(entity, typeof(DestroyTag)))
                {
                    return;
                }

                if (!Blackboard.TryGet(BlackboardEntryId.CameraPosition, out float3 cameraPosition) ||
                    !Blackboard.TryGet(BlackboardEntryId.CameraWidthExtendUnits, out float cameraWidthExtendUnits))
                {
                    return;
                }

                var renderMesh = EntityManager.GetSharedComponentData<RenderMesh>(entity);
                var meshSize = renderMesh.mesh.bounds.size;

                var isBehindCamera = translation.Value.x < cameraPosition.x;

                var maxDistanceToCamera = (meshSize.x * 0.5f) + cameraWidthExtendUnits;
                var distanceToCamera = Mathf.Abs(translation.Value.x - cameraPosition.x);

                if (!isBehindCamera || distanceToCamera < maxDistanceToCamera)
                {
                    return;
                }

                PostUpdateCommands.AddComponent(entity, typeof(DestroyTag));
            });
        }
    }
}
