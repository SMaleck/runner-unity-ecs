using Source.Features.Camera.Config;
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
        private readonly GameCamera _gameCamera;
        private readonly GameCameraConfig _gameCameraConfig;

        public GameCameraController(
            GameCamera gameCamera,
            GameCameraConfig gameCameraConfig)
        {
            _gameCamera = gameCamera;
            _gameCameraConfig = gameCameraConfig;

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

            _gameCamera.transform.position = new Vector3(
                position.x + _gameCameraConfig.FollowOffset.x,
                _gameCamera.transform.position.y + _gameCameraConfig.FollowOffset.y,
                _gameCamera.transform.position.z + _gameCameraConfig.FollowOffset.z);

            Blackboard.Set(BlackboardEntryId.CameraPosition, new float3(_gameCamera.transform.position));
        }
    }
}
