using RollABall.Core.Events;
using RollABall.Presentation.UI.Buttons;
using RollABall.Presentation.UI.Common;
using System;

namespace RollABall.Presentation.UI.Victory
{
    public class VictoryPanelController : IDisposable
    {
        // ─── Campos ───────────────────────────────────────────────────
        private readonly VictoryPanelView _view;
        private readonly IEventBus _eventBus;
        private readonly UIHoverHandler _hoverHandler;

        // ─── Constructor ──────────────────────────────────────────────
        public VictoryPanelController(VictoryPanelView view, UIHoverableButton[] hoverableButtons, IEventBus eventBus)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _hoverHandler = new UIHoverHandler(hoverableButtons);

            _view.NextLevelButton.onClick.AddListener(HandleNextLevelClicked);
            _view.MainMenuButton.onClick.AddListener(HandleMainMenuClicked);
            _view.RestartButton.onClick.AddListener(HandleRestartClicked);

            _eventBus.Subscribe<LevelCompletedEvent>(HandleLevelCompleted);

            _view.Hide();
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleLevelCompleted(LevelCompletedEvent e)
        {
            _view.Show(e.Keys, e.ElapsedSeconds, e.FinalScore);
            _hoverHandler.ResetColors(); // resetea colores al mostrar el panel
        }

        private void HandleNextLevelClicked()
        {
            _eventBus.Publish(new NextLevelRequestedEvent());
        }

        private void HandleMainMenuClicked()
        {
            _eventBus.Publish(new LoadSceneEvent(Domain.Enums.SceneType.MainMenuScene));
        }

        private void HandleRestartClicked()
        {
            _eventBus.Publish(new RestartRequestedEvent());
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _view.NextLevelButton.onClick.RemoveListener(HandleNextLevelClicked);
            _view.MainMenuButton.onClick.RemoveListener(HandleMainMenuClicked);
            _view.RestartButton.onClick.RemoveListener(HandleRestartClicked);
            _eventBus.Unsubscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _hoverHandler.Dispose();
        }
    }
}