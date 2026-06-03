using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using UnityEngine;

namespace RollABall.Presentation.UI.HUD
{
    public class HudInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private HudView _view;

        private HudController _controller;
        private HudModel _model;

        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            _model      = new HudModel();
            _controller = new HudController(_model, _view, eventBus);
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}