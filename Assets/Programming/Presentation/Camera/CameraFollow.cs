using UnityEngine;

namespace RollABall.Presentation.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField, Tooltip("Referencia a la bola")]
        private Transform _ball;

        private Vector3 _offset;

        private void OnValidate()
        {
            Debug.Assert(_ball != null, nameof(_ball));
        }

        private void Start()
        {
            _offset = transform.position - _ball.position;
        }

        private void LateUpdate()
        {
            transform.position = _ball.position + _offset;
        }
    }
}