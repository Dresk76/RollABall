using RollABall.Core.Events;
using RollABall.Presentation.UI.Buttons;
using RollABall.Presentation.UI.Common;
using System;

namespace RollABall.Presentation.UI.Confirmation
{
    public class ConfirmationPanelController : IDisposable
    {
        private readonly ConfirmationPanelView _view;
        private readonly IEventBus _eventBus;
        private readonly UIHoverHandler _hoverHandler;

        public ConfirmationPanelController(ConfirmationPanelView view, UIHoverableButton[] hoverableButtons, IEventBus eventBus)
        {
            _view         = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus     = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _hoverHandler = new UIHoverHandler(hoverableButtons);

            _view.ConfirmButton.onClick.AddListener(HandleConfirm);
            _view.CancelButton.onClick.AddListener(HandleCancel);

            _eventBus.Subscribe<NewGameRequestedEvent>(HandleNewGameRequested);
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleNewGameRequested(NewGameRequestedEvent e) { }

        private void HandleConfirm()
        {
            _eventBus.Publish(new NewGameConfirmedEvent());
        }

        private void HandleCancel()
        {
            _eventBus.Publish(new CloseConfirmationRequestedEvent());
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _view.ConfirmButton.onClick.RemoveListener(HandleConfirm);
            _view.CancelButton.onClick.RemoveListener(HandleCancel);
            _eventBus.Unsubscribe<NewGameRequestedEvent>(HandleNewGameRequested);
            _hoverHandler.Dispose();
        }
    }
}