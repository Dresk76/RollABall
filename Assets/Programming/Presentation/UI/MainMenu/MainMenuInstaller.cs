using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Infrastructure.Save;
using RollABall.Presentation.UI.Buttons;
using UnityEngine;

namespace RollABall.Presentation.UI.MainMenu
{
    public sealed class MainMenuInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private MainMenuView _view;
        [SerializeField] private UIHoverableButton[] _hoverableButtons;

        private MainMenuController _controller;

        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            _controller = new MainMenuController(_view, _hoverableButtons, eventBus);

            var progress      = SaveSystem.Load();
            bool hasActiveGame = progress.HasPlayedBefore &&
                                 progress.NextLevelToPlay > 0;

            eventBus.Publish(new GameReadyEvent(hasActiveGame));
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}