using Source.Features.DataBridge;
using UGF.Util.UniRx;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace Source.Features.Camera
{
    // ToDo Improve tracking, slightly out of sync. Create System if possible
    public class GameCameraController : AbstractDisposable
    {
        private readonly UnityEngine.Camera _camera;

        public GameCameraController(UnityEngine.Camera camera)
        {
            _camera = camera;

            Observable.EveryUpdate()
                .Subscribe(_ => OnUpdate())
                .AddTo(Disposer);
        }

        private void OnUpdate()
        {
            if (!Blackboard.TryGet(BlackboardEntryId.PlayerPosition, out float3 position))
            {
                return;
            }

            _camera.transform.position = new Vector3(
                position.x,
                _camera.transform.position.y,
                _camera.transform.position.z);
        }
    }
}
