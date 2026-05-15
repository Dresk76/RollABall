using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using System;
using UnityEngine;

namespace RollABall.Domain.Gameplay.Environment
{
    public class AreaVictory : MonoBehaviour, IDisposable
    {
        private IEventBus _eventBus;
        private int _finalScore;
        private bool _isActive; // solo activa después de que se abra la puerta

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventBus.Subscribe<TrapDoorOpenEvent>(HandleTrapDoorOpen);
        }

        private void HandleTrapDoorOpen(TrapDoorOpenEvent e)
        {
            _isActive = true; // ahora sí puede detectar la bola
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isActive) return;
            if (!other.CompareTag("Player")) return;

            _isActive = false; // evita publicar dos veces
            _eventBus.Publish(new LevelCompletedEvent(_finalScore));
        }

        public void SetFinalScore(int score)
        {
            _finalScore = score;
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