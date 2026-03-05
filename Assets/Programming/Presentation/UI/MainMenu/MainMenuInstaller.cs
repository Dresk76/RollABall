using RollABall.Programming.Core.Events;
using RollABall.Programming.Core.Interfaces;
using UnityEngine;

namespace RollABall.Programming.UI.MainMenu
{
    public sealed class MainMenuInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private MainMenuView _view;

        private MainMenuController _controller;


        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            _controller = new MainMenuController(_view, eventBus);
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}
