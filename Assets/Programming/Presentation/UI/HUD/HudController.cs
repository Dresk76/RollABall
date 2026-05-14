using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Domain.Enums;
using System;
using TMPro;
using UnityEngine;

namespace RollABall.Presentation.UI.HUD
{
    public class HudController : MonoBehaviour, ISceneInitializable, IDisposable
    {
        [SerializeField] private TextMeshProUGUI _keysText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _timerText;

        private IEventBus _eventBus;

        private void OnValidate()
        {
            Debug.Assert(_keysText != null, nameof(_keysText));
            Debug.Assert(_scoreText != null, nameof(_scoreText));
            Debug.Assert(_timerText != null, nameof(_timerText));
        }

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _eventBus.Subscribe<GameStateChangedEvent>(HandleGameStateChanged);
            _eventBus.Subscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus.Subscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus.Subscribe<TimerUpdatedEvent>(HandleTimerUpdated);
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleGameStateChanged(GameStateChangedEvent e)
        {
            gameObject.SetActive(e.NewState == GameState.Playing);
        }

        private void HandleKeyCollected(KeyCollectedEvent e)
        {
            _keysText.text = $"Llaves: {e.KeyValue}";
        }

        private void HandleLevelCompleted(LevelCompletedEvent e)
        {
            _scoreText.text = $"Score: {e.FinalScore}";
        }

        private void HandleTimerUpdated(TimerUpdatedEvent e)
        {
            TimeSpan time = TimeSpan.FromSeconds(e.ElapsedSeconds);
            _timerText.text = $"{time.Minutes:00}:{time.Seconds:00}";
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _eventBus?.Unsubscribe<GameStateChangedEvent>(HandleGameStateChanged);
            _eventBus?.Unsubscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus?.Unsubscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus?.Unsubscribe<TimerUpdatedEvent>(HandleTimerUpdated);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}