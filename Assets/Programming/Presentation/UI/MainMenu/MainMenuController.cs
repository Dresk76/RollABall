using RollABall.Core.Events;
using RollABall.Presentation.UI.Buttons;
using System;

namespace RollABall.Presentation.UI.MainMenu
{
    public class MainMenuController : IDisposable
    {
        private struct HoverActions
        {
            public Action OnEnter;
            public Action OnExit;
        }

        private readonly MainMenuView _view;
        private readonly UIHoverableButton[] _hoverableButtons;
        private readonly IEventBus _eventBus;
        private readonly HoverActions[] _hoverActions;

        public MainMenuController(MainMenuView view, UIHoverableButton[] hoverableButtons, IEventBus eventBus)
        //                        ↑ ya no recibe UIButtonStyle global
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _hoverableButtons = hoverableButtons ?? throw new ArgumentNullException(nameof(hoverableButtons));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _view.StartGameButton.onClick.AddListener(HandleStartGameButtonClicked);

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
                button.Text.color = button.Style.NormalColor; // ← usa su propio estilo
            }
        }

        // ─── Handlers ─────────────────────────────────────────────────────
        private void HandleStartGameButtonClicked()
        {
            _eventBus.Publish(new StartGameRequestedEvent());
        }

        private void OnHoverEntered(UIHoverableButton button)
        {
            button.Text.color = button.Style.HoverColor;  // ← su propio HoverColor
        }

        private void OnHoverExited(UIHoverableButton button)
        {
            button.Text.color = button.Style.NormalColor; // ← su propio NormalColor
        }

        // ─── Ciclo de vida ────────────────────────────────────────────────
        public void Dispose()
        {
            _view.StartGameButton.onClick.RemoveListener(HandleStartGameButtonClicked);

            for (int i = 0; i < _hoverableButtons.Length; i++)
            {
                _hoverableButtons[i].Hoverable.HoverEntered -= _hoverActions[i].OnEnter;
                _hoverableButtons[i].Hoverable.HoverExited  -= _hoverActions[i].OnExit;
            }
        }
    }
}