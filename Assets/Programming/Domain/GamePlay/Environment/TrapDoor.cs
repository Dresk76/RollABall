using RollABall.Core.Events;
using System;
using UnityEngine;

namespace RollABall.Domain.Gameplay.Environment
{
    public class TrapDoor : MonoBehaviour, IDisposable
    {
        private IEventBus _eventBus;

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventBus.Subscribe<TrapDoorOpenEvent>(HandleTrapDoorOpen);
        }

        private void HandleTrapDoorOpen(TrapDoorOpenEvent e)
        {
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _eventBus?.Unsubscribe<TrapDoorOpenEvent>(HandleTrapDoorOpen);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}