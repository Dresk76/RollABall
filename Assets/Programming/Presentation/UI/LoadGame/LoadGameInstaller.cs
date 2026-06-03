using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Infrastructure.Configuration;
using RollABall.Presentation.UI.Buttons;
using UnityEngine;

namespace RollABall.Presentation.UI.LoadGame
{
    public class LoadGameInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private LoadGameView _view;
        [SerializeField] private LevelProgressionConfig _levelConfig;
        [SerializeField] private UIHoverableButton[] _hoverableButtons;

        private LoadGameController _controller;

        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
            Debug.Assert(_levelConfig != null, nameof(_levelConfig));
        }

        public void Initialize(IEventBus eventBus)
        {
            _controller = new LoadGameController(_view, _levelConfig, _hoverableButtons, eventBus);
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}