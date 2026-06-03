using RollABall.Core.Events;
using RollABall.Presentation.UI.Buttons;
using RollABall.Presentation.UI.Common;
using System;

namespace RollABall.Presentation.UI.MainMenu
{
    public class MainMenuController : IDisposable
    {
        // ─── Campos ───────────────────────────────────────────────────
        private readonly MainMenuView _view;
        private readonly IEventBus _eventBus;
        private readonly UIHoverHandler _hoverHandler;

        private bool _hasActiveGame;

        // ─── Constructor ──────────────────────────────────────────────
        public MainMenuController(MainMenuView view, UIHoverableButton[] hoverableButtons, IEventBus eventBus)
        {
            _view     = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _hoverHandler = new UIHoverHandler(hoverableButtons);

            _view.PlayButton.onClick.AddListener(HandlePlayButtonClicked);
            _view.OptionsButton.onClick.AddListener(HandleOptionsButtonClicked);
            _view.NewGameButton.onClick.AddListener(HandleNewGameButtonClicked);
            _view.LoadGameButton.onClick.AddListener(HandleLoadGameButtonClicked);
            _view.QuitButton.onClick.AddListener(HandleQuitButtonClicked);

            _eventBus.Subscribe<GameReadyEvent>(HandleGameReady);
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleGameReady(GameReadyEvent e)
        {
            _hasActiveGame = e.HasActiveGame;
            _view.SetPlayButtonText(_hasActiveGame);
        }

        private void HandlePlayButtonClicked()
        {
            if (_hasActiveGame)
                _eventBus.Publish(new ResumeGameRequestedEvent());
            else
                _eventBus.Publish(new StartGameRequestedEvent());
        }

        private void HandleOptionsButtonClicked()
        {
            _eventBus.Publish(new OpenOptionsRequestedEvent());
        }

        private void HandleNewGameButtonClicked()
        {
            _eventBus.Publish(new NewGameRequestedEvent());
        }

        private void HandleLoadGameButtonClicked()
        {
            _eventBus.Publish(new OpenLoadGameRequestedEvent());
        }

        private void HandleQuitButtonClicked()
        {
            _eventBus.Publish(new QuitGameRequestedEvent());
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _view.PlayButton.onClick.RemoveListener(HandlePlayButtonClicked);
            _view.OptionsButton.onClick.RemoveListener(HandleOptionsButtonClicked);
            _view.NewGameButton.onClick.RemoveListener(HandleNewGameButtonClicked);
            _view.LoadGameButton.onClick.RemoveListener(HandleLoadGameButtonClicked);
            _view.QuitButton.onClick.RemoveListener(HandleQuitButtonClicked);
            _eventBus.Unsubscribe<GameReadyEvent>(HandleGameReady);
            _hoverHandler.Dispose();
        }
    }
}