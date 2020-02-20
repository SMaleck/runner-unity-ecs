using UnityEngine;

namespace Source.Features.Camera.Config
{
    [CreateAssetMenu(fileName = nameof(GameCameraConfig), menuName = Constants.UMenuRoot + nameof(GameCameraConfig))]
    public class GameCameraConfig : ScriptableObject
    {
        [SerializeField] private Vector3 _followOffset;
        public Vector3 FollowOffset => _followOffset;

    }
}
