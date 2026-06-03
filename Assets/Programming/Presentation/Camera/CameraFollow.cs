using UnityEngine;

namespace RollABall.Presentation.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _ball;
        private Vector3 _offset;

        private void OnValidate()
        {
            Debug.Assert(_ball != null || true, "Ball will be set via installer");
        }

        public void SetTarget(Transform ball)
        {
            _ball  = ball;
            _offset = transform.position - _ball.position;
        }

        private void LateUpdate()
        {
            if (_ball == null) return;
            transform.position = _ball.position + _offset;
        }
    }
}