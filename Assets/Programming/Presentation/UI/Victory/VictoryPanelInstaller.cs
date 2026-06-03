using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Presentation.UI.Buttons;
using UnityEngine;

namespace RollABall.Presentation.UI.Victory
{
    public class VictoryPanelInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private VictoryPanelView _view;
        [SerializeField] private UIHoverableButton[] _hoverableButtons;

        private VictoryPanelController _controller;

        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            _controller = new VictoryPanelController(_view, _hoverableButtons, eventBus);
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}