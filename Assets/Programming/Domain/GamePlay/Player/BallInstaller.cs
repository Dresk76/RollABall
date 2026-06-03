// BallInstaller.cs
using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using UnityEngine;

namespace RollABall.Domain.Gameplay.Player
{
    public class BallInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private BallController _ballController;

        private void OnValidate()
        {
            Debug.Assert(_ballController != null, nameof(_ballController));
        }

        public void Initialize(IEventBus eventBus)
        {
            _ballController.Initialize(eventBus);
        }
    }
}