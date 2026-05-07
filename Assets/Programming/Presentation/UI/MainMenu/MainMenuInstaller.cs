using RollABall.Programming.Core.Events;
using RollABall.Programming.Core.Interfaces;
using RollABall.Programming.UI.Buttons;
using UnityEngine;

namespace RollABall.Programming.UI.MainMenu
{
    public sealed class MainMenuInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private MainMenuView _view;
        [SerializeField] private UIHoverable[] _hoverables;
        [SerializeField] private UIButtonStyle _style;

        private MainMenuController _controller;


        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            _controller = new MainMenuController(_view, _hoverables, _style, eventBus);
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}
