using UnityEngine;

namespace Source.Features.Camera
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public float WidthUnits => HeightUnits * _camera.aspect;

        public float HeightUnits => _camera.orthographic
            ? GetHeightOrthographic()
            : GetHeightPerspective();

        private float GetHeightOrthographic()
        {
            return 2f * _camera.orthographicSize;
        }
        
        private float GetHeightPerspective()
        {
            var distance = Mathf.Abs(_camera.transform.position.z);
            return 2.0f * distance * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }
    }
}
