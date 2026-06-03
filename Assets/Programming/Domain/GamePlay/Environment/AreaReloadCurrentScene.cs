using RollABall.Core.Events;
using System;
using UnityEngine;

namespace RollABall.Domain.Gameplay.Environment
{
    public class AreaReloadCurrentScene : MonoBehaviour, IDisposable
    {
        private IEventBus _eventBus;

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _eventBus.Publish(new FallRestartEvent());
        }

        public void Dispose()
        {
            _eventBus = null;
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}