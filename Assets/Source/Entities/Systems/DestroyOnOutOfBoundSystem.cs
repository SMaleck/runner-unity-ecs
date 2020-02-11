using Source.Entities.Components;
using Source.Features.DataBridge;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace Source.Entities.Systems
{
    [UpdateAfter(typeof(BuildPhysicsWorld))]
    [UpdateAfter(typeof(TravelStatsBridgeSystem))]
    public class DestroyOnOutOfBoundSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity entity,
                ref KillThresholdX killThresholdX,
                ref Translation translation) =>
            {
                if (!Blackboard.TryGet(BlackboardEntryId.CameraPosition, out float3 cameraPosition))
                {
                    return;
                }

                var isBehindCamera = translation.Value.x < cameraPosition.x;
                var distanceToCamera = Mathf.Abs(translation.Value.x - cameraPosition.x);

                if (!isBehindCamera || distanceToCamera < killThresholdX.Value)
                {
                    return;
                }

                PostUpdateCommands.DestroyEntity(entity);
            });
        }
    }
}
