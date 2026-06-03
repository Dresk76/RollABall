using RollABall.Core.Events;
using RollABall.Infrastructure.Configuration;
using RollABall.Infrastructure.Save;
using RollABall.Presentation.UI.Buttons;
using RollABall.Presentation.UI.Common;
using System;

namespace RollABall.Presentation.UI.LoadGame
{
    public class LoadGameController : IDisposable
    {
        private readonly LoadGameView _view;
        private readonly IEventBus _eventBus;
        private readonly LevelProgressionConfig _levelConfig;
        private readonly UIHoverHandler _hoverHandler;

        public LoadGameController(LoadGameView view, LevelProgressionConfig levelConfig, UIHoverableButton[] hoverableButtons, IEventBus eventBus)
        {
            _view         = view ?? throw new ArgumentNullException(nameof(view));
            _levelConfig  = levelConfig ?? throw new ArgumentNullException(nameof(levelConfig));
            _eventBus     = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _hoverHandler = new UIHoverHandler(hoverableButtons);

            _view.PrevButton.onClick.AddListener(HandlePrev);
            _view.NextButton.onClick.AddListener(HandleNext);
            _view.BackButton.onClick.AddListener(HandleBack);
            _view.LevelEntry.Button.onClick.AddListener(HandleLevelSelected);

            _eventBus.Subscribe<OpenLoadGameRequestedEvent>(HandleOpenLoadGame);

            _view.Setup(_levelConfig.TotalLevels);
            RefreshCurrentEntry();
        }

        // ─── Setup ────────────────────────────────────────────────────
        private void RefreshCurrentEntry()
        {
            var progress    = SaveSystem.Load();
            int index       = _view.GetCurrentIndex();
            bool isUnlocked = progress.UnlockedLevels.Contains(index);
            string name     = _levelConfig.GetLevelName(index);
            var sprite      = _levelConfig.GetLevelSprite(index);

            _view.LevelEntry.Setup(name, sprite, isUnlocked);
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleOpenLoadGame(OpenLoadGameRequestedEvent e)
        {
            _view.ResetCarousel();
            RefreshCurrentEntry();
        }

        private void HandlePrev()
        {
            _view.ScrollToPrev();
            RefreshCurrentEntry();
        }

        private void HandleNext()
        {
            _view.ScrollToNext();
            RefreshCurrentEntry();
        }

        private void HandleLevelSelected()
        {
            int index = _view.GetCurrentIndex();
            _eventBus.Publish(new LoadSpecificLevelRequestedEvent(index));
        }

        private void HandleBack()
        {
            _eventBus.Publish(new CloseLoadGameRequestedEvent());
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _view.PrevButton.onClick.RemoveListener(HandlePrev);
            _view.NextButton.onClick.RemoveListener(HandleNext);
            _view.BackButton.onClick.RemoveListener(HandleBack);
            _view.LevelEntry.Button.onClick.RemoveListener(HandleLevelSelected);
            _eventBus.Unsubscribe<OpenLoadGameRequestedEvent>(HandleOpenLoadGame);
            _hoverHandler.Dispose();
        }
    }
}