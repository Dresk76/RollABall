using RollABall.Core.Events;
using System;
using UnityEngine;

namespace RollABall.Domain.Gameplay.Level
{
    public class LevelController : IDisposable
    {
        // ─── Campos ───────────────────────────────────────────────────
        private readonly IEventBus _eventBus;
        private readonly int _maxScore;

        private float _elapsedTime;
        private bool _isRunning;

        // ─── Constructor ──────────────────────────────────────────────
        public LevelController(IEventBus eventBus, int maxScore)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _maxScore = maxScore;

            _eventBus.Subscribe<TrapDoorOpenEvent>(HandleTrapDoorOpen);

            _isRunning = true;
        }

        // ─── Tick ─────────────────────────────────────────────────────
        public void Tick(float deltaTime)
        {
            if (!_isRunning) return;

            _elapsedTime += deltaTime;
            _eventBus.Publish(new TimerUpdatedEvent(_elapsedTime));
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleTrapDoorOpen(TrapDoorOpenEvent e)
        {
            _isRunning = false;

            int finalScore = CalculateScore(_elapsedTime, _maxScore);
            _eventBus.Publish(new LevelCompletedEvent(finalScore));
        }

        // ─── Helpers privados ─────────────────────────────────────────
        private int CalculateScore(float time, int maxScore)
        {
            int score = maxScore - Mathf.RoundToInt(time * 10f);
            return Mathf.Max(score, 0);
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _eventBus?.Unsubscribe<TrapDoorOpenEvent>(HandleTrapDoorOpen);
        }
    }
}