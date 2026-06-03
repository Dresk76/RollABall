using RollABall.Core.Events;
using RollABall.Infrastructure.Configuration;
using RollABall.Presentation.UI.Buttons;
using RollABall.Presentation.UI.Common;
using System;

namespace RollABall.Presentation.UI.Pause
{
    public class PausePanelController : IDisposable
    {
        private readonly PausePanelModel _model;
        private readonly PausePanelView _view;
        private readonly IEventBus _eventBus;
        private readonly UIHoverHandler _hoverHandler;

        public PausePanelController(PausePanelModel model, PausePanelView view, UIHoverableButton[] hoverableButtons, IEventBus eventBus)
        {
            _model        = model ?? throw new ArgumentNullException(nameof(model));
            _view         = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus     = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _hoverHandler = new UIHoverHandler(hoverableButtons);

            // Botones
            _view.ResumeButton.onClick.AddListener(HandleResume);
            _view.RestartButton.onClick.AddListener(HandleRestart);
            _view.OptionsButton.onClick.AddListener(HandleOptions);
            _view.MainMenuButton.onClick.AddListener(HandleMainMenu);

            // Eventos del Model
            _model.OnLevelNameChanged += HandleLevelNameChanged;
            _model.OnTimerChanged     += HandleTimerChanged;
            _model.OnKeysChanged      += HandleKeysChanged;

            // Eventos del Bus
            _eventBus.Subscribe<GameStateChangedEvent>(HandleGameStateChanged);
            _eventBus.Subscribe<TimerUpdatedEvent>(HandleTimerUpdated);
            _eventBus.Subscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus.Subscribe<LevelLoadedEvent>(HandleLevelLoaded);
        }

        // ─── Handlers EventBus ────────────────────────────────────────
        private void HandleGameStateChanged(GameStateChangedEvent e)
        {
            if (e.NewState == Domain.Enums.GameState.Paused)
            {
                // Refresca datos al pausar
                _view.SetTimer(_model.ElapsedSeconds);
                _view.SetKeys(_model.CollectedKeys);
            }
        }

        private void HandleTimerUpdated(TimerUpdatedEvent e)
        {
            _model.SetTimer(e.ElapsedSeconds);
        }

        private void HandleKeyCollected(KeyCollectedEvent e)
        {
            _model.SetKeys(e.KeyValue);
        }

        private void HandleLevelLoaded(LevelLoadedEvent e)
        {
            _model.SetLevelName(e.LevelName);
        }

        // ─── Handlers Model ───────────────────────────────────────────
        private void HandleLevelNameChanged(string levelName)
        {
            _view.SetLevelName(levelName);
        }

        private void HandleTimerChanged(float elapsedSeconds)
        {
            // Solo actualiza la View si el panel está pausado
        }

        private void HandleKeysChanged(int keys)
        {
            // Solo actualiza la View si el panel está pausado
        }

        // ─── Handlers botones ─────────────────────────────────────────
        private void HandleResume()
        {
            _eventBus.Publish(new PauseRequestedEvent(false));
        }

        private void HandleRestart()
        {
            _eventBus.Publish(new RestartRequestedEvent());
        }

        private void HandleOptions()
        {
            _eventBus.Publish(new OpenOptionsRequestedEvent());
        }

        private void HandleMainMenu()
        {
            _eventBus.Publish(new LoadSceneEvent(Domain.Enums.SceneType.MainMenuScene));
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _view.ResumeButton.onClick.RemoveListener(HandleResume);
            _view.RestartButton.onClick.RemoveListener(HandleRestart);
            _view.OptionsButton.onClick.RemoveListener(HandleOptions);
            _view.MainMenuButton.onClick.RemoveListener(HandleMainMenu);

            _model.OnLevelNameChanged -= HandleLevelNameChanged;
            _model.OnTimerChanged     -= HandleTimerChanged;
            _model.OnKeysChanged      -= HandleKeysChanged;

            _eventBus?.Unsubscribe<GameStateChangedEvent>(HandleGameStateChanged);
            _eventBus?.Unsubscribe<TimerUpdatedEvent>(HandleTimerUpdated);
            _eventBus?.Unsubscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus?.Unsubscribe<LevelLoadedEvent>(HandleLevelLoaded);

            _hoverHandler.Dispose();
        }
    }
}