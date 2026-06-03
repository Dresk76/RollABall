using RollABall.Core.Events;
using System;

namespace RollABall.Presentation.UI.HUD
{
    public class HudController : IDisposable
    {
        private readonly HudModel _model;
        private readonly HudView _view;
        private readonly IEventBus _eventBus;

        public HudController(HudModel model, HudView view, IEventBus eventBus)
        {
            _model    = model ?? throw new ArgumentNullException(nameof(model));
            _view     = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _model.OnKeysChanged  += HandleKeysChanged;
            _model.OnTimerChanged += HandleTimerChanged;
            _model.OnScoreChanged += HandleScoreChanged;

            _eventBus.Subscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus.Subscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus.Subscribe<TimerUpdatedEvent>(HandleTimerUpdated);
            _eventBus.Subscribe<LevelLoadedEvent>(HandleLevelLoaded);
        }

        // ─── Handlers EventBus ────────────────────────────────────────
        private void HandleKeyCollected(KeyCollectedEvent e)
        {
            _model.SetKeys(e.KeyValue);
        }

        private void HandleLevelCompleted(LevelCompletedEvent e)
        {
            _model.SetScore(e.FinalScore);
        }

        private void HandleTimerUpdated(TimerUpdatedEvent e)
        {
            _model.SetTimer(e.ElapsedSeconds);
        }

        private void HandleLevelLoaded(LevelLoadedEvent e)
        {
            _model.Reset();
        }

        // ─── Handlers Model ───────────────────────────────────────────
        private void HandleKeysChanged(int keys)
        {
            _view.SetKeys(keys);
        }

        private void HandleTimerChanged(float elapsedSeconds)
        {
            _view.SetTimer(elapsedSeconds);
        }

        private void HandleScoreChanged(int score)
        {
            _view.SetScore(score);
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _model.OnKeysChanged  -= HandleKeysChanged;
            _model.OnTimerChanged -= HandleTimerChanged;
            _model.OnScoreChanged -= HandleScoreChanged;

            _eventBus?.Unsubscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus?.Unsubscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus?.Unsubscribe<TimerUpdatedEvent>(HandleTimerUpdated);
            _eventBus?.Unsubscribe<LevelLoadedEvent>(HandleLevelLoaded);
        }
    }
}