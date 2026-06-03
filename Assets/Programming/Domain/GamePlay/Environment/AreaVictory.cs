using RollABall.Core.Events;
using System;
using UnityEngine;

namespace RollABall.Domain.Gameplay.Environment
{
    public class AreaVictory : MonoBehaviour, IDisposable
    {
        private IEventBus _eventBus;
        private bool _isActive;

        private int _finalScore;
        private int _keys;
        private float _elapsedSeconds;

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventBus.Subscribe<TrapDoorOpenEvent>(HandleTrapDoorOpen);
        }

        private void HandleTrapDoorOpen(TrapDoorOpenEvent e)
        {
            _isActive = true;
        }

        public void SetLevelResults(int finalScore, int keys, float elapsedSeconds)
        {
            _finalScore    = finalScore;
            _keys          = keys;
            _elapsedSeconds = elapsedSeconds;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isActive) return;
            if (!other.CompareTag("Player")) return;

            _isActive = false;
            _eventBus.Publish(new LevelCompletedEvent(_finalScore, _keys, _elapsedSeconds));
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