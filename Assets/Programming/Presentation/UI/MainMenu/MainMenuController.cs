using RollABall.Programming.Core.Events;
using System;


// TRTATAR DE IMPLEMENTAR LA INTERFACE IInitializable
namespace RollABall.Programming.UI.MainMenu
{
    public class MainMenuController : IDisposable
    {
        private readonly MainMenuView _view;
        private readonly IEventBus _eventBus;

        public MainMenuController(MainMenuView view, IEventBus eventBus)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _view.StartGameButton.onClick.AddListener(HandleStartGameButtonClicked);
        }

        private void HandleStartGameButtonClicked()
        {
            _eventBus.Publish(new StartGameRequestedEvent());
        }

        public void Dispose()
        {
            _view.StartGameButton.onClick.RemoveListener(HandleStartGameButtonClicked);
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
