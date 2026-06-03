using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Domain.Enums;
using System;
using UnityEngine;

namespace RollABall.Presentation.UI.Common
{
    public class GameUIManager : MonoBehaviour, ISceneInitializable, IDisposable
    {
        [Header("PANELS")]
        [SerializeField] private GameObject _hudPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _optionsPanel;
        [SerializeField] private GameObject _victoryPanel;

        private IEventBus _eventBus;

        private void OnValidate()
        {
            Debug.Assert(_hudPanel != null, nameof(_hudPanel));
            Debug.Assert(_pausePanel != null, nameof(_pausePanel));
            Debug.Assert(_optionsPanel != null, nameof(_optionsPanel));
            Debug.Assert(_victoryPanel != null, nameof(_victoryPanel));
        }

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _eventBus.Subscribe<GameStateChangedEvent>(HandleGameStateChanged);
            _eventBus.Subscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus.Subscribe<OpenOptionsRequestedEvent>(HandleOpenOptions);
            _eventBus.Subscribe<CloseOptionsRequestedEvent>(HandleCloseOptions);

            ShowHUD();
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleGameStateChanged(GameStateChangedEvent e)
        {
            switch (e.NewState)
            {
                case GameState.Playing:
                    ShowHUD();
                    break;

                case GameState.Paused:
                    ShowPause();
                    break;

                case GameState.GameOver:
                    HideAll();
                    break;
            }
        }

        private void HandleLevelCompleted(LevelCompletedEvent e)
        {
            HideAll();
            _victoryPanel.SetActive(true);
        }

        private void HandleOpenOptions(OpenOptionsRequestedEvent e)
        {
            _pausePanel.SetActive(false);
            _optionsPanel.SetActive(true);
        }

        private void HandleCloseOptions(CloseOptionsRequestedEvent e)
        {
            _optionsPanel.SetActive(false);
            _pausePanel.SetActive(true);
        }

        // ─── Helpers ──────────────────────────────────────────────────
        private void ShowHUD()
        {
            _hudPanel.SetActive(true);
            _pausePanel.SetActive(false);
            _optionsPanel.SetActive(false);
            _victoryPanel.SetActive(false);
        }

        private void ShowPause()
        {
            _hudPanel.SetActive(false);
            _pausePanel.SetActive(true);
            _optionsPanel.SetActive(false);
            _victoryPanel.SetActive(false);
        }

        private void HideAll()
        {
            _hudPanel.SetActive(false);
            _pausePanel.SetActive(false);
            _optionsPanel.SetActive(false);
            _victoryPanel.SetActive(false);
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _eventBus?.Unsubscribe<GameStateChangedEvent>(HandleGameStateChanged);
            _eventBus?.Unsubscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus?.Unsubscribe<OpenOptionsRequestedEvent>(HandleOpenOptions);
            _eventBus?.Unsubscribe<CloseOptionsRequestedEvent>(HandleCloseOptions);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}