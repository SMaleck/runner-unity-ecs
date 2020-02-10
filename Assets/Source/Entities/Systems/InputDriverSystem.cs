using Source.Entities.Components;
using Source.Entities.Components.Tags;
using Unity.Entities;
using UnityEngine;

namespace Source.Entities.Systems
{
    [UpdateBefore(typeof(MovementSystem))]
    public class InputDriverSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref InputDriverTag inputDriverTag, ref JumpIntent jumpIntent) =>
            {
                // ToDo [ECS] cleanup input usage
                if (Input.GetKeyDown("a"))
                {
                    jumpIntent.HasIntent = true;
                }
            });
        }
    }
}
