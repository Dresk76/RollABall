using RollABall.Core.Events;
using RollABall.Presentation.UI.Buttons;
using System;

namespace RollABall.Presentation.UI.MainMenu
{
    public class MainMenuController : IDisposable
    {
        // ─── Estructura interna ───────────────────────────────────────
        private struct HoverActions
        {
            public Action OnEnter;
            public Action OnExit;
        }

        // ─── Campos ───────────────────────────────────────────────────
        private readonly MainMenuView _view;
        private readonly UIHoverableButton[] _hoverableButtons;
        private readonly IEventBus _eventBus;
        private readonly HoverActions[] _hoverActions;

        private bool _hasActiveGame;

        // ─── Constructor ──────────────────────────────────────────────
        public MainMenuController(MainMenuView view, UIHoverableButton[] hoverableButtons, IEventBus eventBus)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _hoverableButtons = hoverableButtons ?? throw new ArgumentNullException(nameof(hoverableButtons));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            // Botones
            _view.StartGameButton.onClick.AddListener(HandleStartGameButtonClicked);
            _view.QuitGameButton.onClick.AddListener(HandleQuitGameButtonClicked);

            // Hovers
            _hoverActions = new HoverActions[_hoverableButtons.Length];

            for (int i = 0; i < _hoverableButtons.Length; i++)
            {
                UIHoverableButton button = _hoverableButtons[i];

                _hoverActions[i] = new HoverActions
                {
                    OnEnter = () => OnHoverEntered(button),
                    OnExit  = () => OnHoverExited(button)
                };

                button.Hoverable.HoverEntered += _hoverActions[i].OnEnter;
                button.Hoverable.HoverExited  += _hoverActions[i].OnExit;
                button.Text.color = button.Style.NormalColor;
            }

            _eventBus.Subscribe<GameReadyEvent>(HandleGameReady);
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleGameReady(GameReadyEvent e)
        {
            _hasActiveGame = e.HasActiveGame;
            _view.SetPlayButtonText(_hasActiveGame);
        }

        private void HandleStartGameButtonClicked()
        {
            if (_hasActiveGame)
                _eventBus.Publish(new ResumeGameRequestedEvent());
            else
                _eventBus.Publish(new StartGameRequestedEvent());
        }

        private void HandleQuitGameButtonClicked()
        {
            _eventBus.Publish(new QuitGameRequestedEvent());
        }

        private void OnHoverEntered(UIHoverableButton button)
        {
            button.Text.color = button.Style.HoverColor;
        }

        private void OnHoverExited(UIHoverableButton button)
        {
            button.Text.color = button.Style.NormalColor;
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _view.StartGameButton.onClick.RemoveListener(HandleStartGameButtonClicked);
            _view.QuitGameButton.onClick.RemoveListener(HandleQuitGameButtonClicked);
            _eventBus.Unsubscribe<GameReadyEvent>(HandleGameReady);

            for (int i = 0; i < _hoverableButtons.Length; i++)
            {
                _hoverableButtons[i].Hoverable.HoverEntered -= _hoverActions[i].OnEnter;
                _hoverableButtons[i].Hoverable.HoverExited  -= _hoverActions[i].OnExit;
            }
        }
    }
}