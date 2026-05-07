using RollABall.Programming.Core.Events;
using RollABall.Programming.UI.Buttons;
using System;


// TRTATAR DE IMPLEMENTAR LA INTERFACE IInitializable
namespace RollABall.Programming.UI.MainMenu
{
    public class MainMenuController : IDisposable
    {
        private readonly MainMenuView _view;
        private readonly UIHoverable[] _hoverables;
        private readonly UIButtonStyle _style;
        private readonly IEventBus _eventBus;

        public MainMenuController(MainMenuView view, UIHoverable[] hoverables, UIButtonStyle style, IEventBus eventBus)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _hoverables = hoverables ?? throw new ArgumentNullException(nameof(hoverables));
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _view.StartGameButton.onClick.AddListener(HandleStartGameButtonClicked);

            foreach (UIHoverable hoverable in _hoverables)
            {
                hoverable.HoverEntered += OnHoverEntered;
            }

            _view.SetTextColor(_style.NormalColor);
        }

        private void HandleStartGameButtonClicked()
        {
            _eventBus.Publish(new StartGameRequestedEvent());
        }

        private void OnHoverEntered()
        {
            _view.SetTextColor(_style.HoverColor);
        }

        public void Dispose()
        {
            _view.StartGameButton.onClick.RemoveListener(HandleStartGameButtonClicked);
            foreach (UIHoverable hoverable in _hoverables)
            {
                hoverable.HoverEntered -= OnHoverEntered;
            }
        }
    }
}

/// <summary>
/// ?? significa:
/// 
/// 👉 “Si lo de la izquierda existe, úsalo.
/// 👉 Si no existe (es null), usa lo de la derecha.”
/// 
/// Ejemplo simple:
/// 
/// string name = userName ?? "Invitado";
/// 
/// Eso significa:
///
/// Si userName tiene valor → usa ese valor.
/// 
/// Si es null → usa "Invitado".
/// </summary>
