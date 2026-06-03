// CameraInstaller.cs
using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using UnityEngine;

namespace RollABall.Presentation.Camera
{
    public class CameraInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private Transform _ball;

        private void OnValidate()
        {
            Debug.Assert(_cameraFollow != null, nameof(_cameraFollow));
            Debug.Assert(_ball != null, nameof(_ball));
        }

        public void Initialize(IEventBus eventBus)
        {
            _cameraFollow.SetTarget(_ball);
        }
    }
}