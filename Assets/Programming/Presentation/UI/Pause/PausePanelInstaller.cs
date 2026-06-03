using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Presentation.UI.Buttons;
using UnityEngine;

namespace RollABall.Presentation.UI.Pause
{
    public class PausePanelInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private PausePanelView _view;
        [SerializeField] private UIHoverableButton[] _hoverableButtons;

        private PausePanelController _controller;
        private PausePanelModel _model;

        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            _model      = new PausePanelModel();
            _controller = new PausePanelController(_model, _view, _hoverableButtons, eventBus);
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}