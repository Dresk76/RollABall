using RollABall.Core.Events;
using RollABall.Domain.Gameplay.Environment;
using System;
using UnityEngine;

namespace RollABall.Domain.Gameplay.Level
{
    public class LevelController : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly int _maxScore;
        private readonly AreaVictory _areaVictory; // ← referencia al área

        private float _elapsedTime;
        private bool _isRunning;

        public LevelController(IEventBus eventBus, int maxScore, AreaVictory areaVictory)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _maxScore = maxScore;
            _areaVictory = areaVictory ?? throw new ArgumentNullException(nameof(areaVictory));

            _eventBus.Subscribe<TrapDoorOpenEvent>(HandleTrapDoorOpen);
            _isRunning = true;
        }

        public void Tick(float deltaTime)
        {
            if (!_isRunning) return;

            _elapsedTime += deltaTime;
            _eventBus.Publish(new TimerUpdatedEvent(_elapsedTime));
        }

        private void HandleTrapDoorOpen(TrapDoorOpenEvent e)
        {
            _isRunning = false;

            int finalScore = CalculateScore(_elapsedTime, _maxScore);
            _areaVictory.SetFinalScore(finalScore); // ← le pasa el score al área
        }

        private int CalculateScore(float time, int maxScore)
        {
            int score = maxScore - Mathf.RoundToInt(time * 10f);
            return Mathf.Max(score, 0);
        }

        public void Dispose()
        {
            _eventBus?.Unsubscribe<TrapDoorOpenEvent>(HandleTrapDoorOpen);
        }
    }
}