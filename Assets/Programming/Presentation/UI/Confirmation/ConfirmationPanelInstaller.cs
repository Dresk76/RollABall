using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Presentation.UI.Buttons;
using UnityEngine;

namespace RollABall.Presentation.UI.Confirmation
{
    public class ConfirmationPanelInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private ConfirmationPanelView _view;
        [SerializeField] private UIHoverableButton[] _hoverableButtons;

        private ConfirmationPanelController _controller;

        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            _controller = new ConfirmationPanelController(_view, _hoverableButtons, eventBus);
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}