using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Infrastructure.Save;
using System;
using UnityEngine;

namespace RollABall.Presentation.UI.Common
{
    public class UIManager : MonoBehaviour, ISceneInitializable, IDisposable
    {
        [Header("MAIN MENU PANELS")]
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _optionsPanel;
        [SerializeField] private GameObject _loadGamePanel;
        [SerializeField] private GameObject _confirmationPanel;

        [Header("MAIN MENU BUTTONS")]
        [SerializeField] private GameObject _newGameButton;
        [SerializeField] private GameObject _loadGameButton;

        private IEventBus _eventBus;

        private void OnValidate()
        {
            Debug.Assert(_mainMenuPanel != null, nameof(_mainMenuPanel));
            Debug.Assert(_optionsPanel != null, nameof(_optionsPanel));
            Debug.Assert(_loadGamePanel != null, nameof(_loadGamePanel));
            Debug.Assert(_confirmationPanel != null, nameof(_confirmationPanel));
            Debug.Assert(_newGameButton != null, nameof(_newGameButton));
            Debug.Assert(_loadGameButton != null, nameof(_loadGameButton));
        }

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _eventBus.Subscribe<OpenOptionsRequestedEvent>(HandleOpenOptions);
            _eventBus.Subscribe<CloseOptionsRequestedEvent>(HandleCloseOptions);
            _eventBus.Subscribe<OpenLoadGameRequestedEvent>(HandleOpenLoadGame);
            _eventBus.Subscribe<CloseLoadGameRequestedEvent>(HandleCloseLoadGame);
            _eventBus.Subscribe<NewGameRequestedEvent>(HandleNewGameRequested);
            _eventBus.Subscribe<CloseConfirmationRequestedEvent>(HandleCloseConfirmation);
            _eventBus.Subscribe<NewGameConfirmedEvent>(HandleNewGameConfirmed);

            // Estado inicial
            ShowMainMenu();
            UpdateButtonVisibility();
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleOpenOptions(OpenOptionsRequestedEvent e)
        {
            _mainMenuPanel.SetActive(false);
            _optionsPanel.SetActive(true);
        }

        private void HandleCloseOptions(CloseOptionsRequestedEvent e)
        {
            _optionsPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        private void HandleOpenLoadGame(OpenLoadGameRequestedEvent e)
        {
            _mainMenuPanel.SetActive(false);
            _loadGamePanel.SetActive(true);
        }

        private void HandleCloseLoadGame(CloseLoadGameRequestedEvent e)
        {
            _loadGamePanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        private void HandleNewGameRequested(NewGameRequestedEvent e)
        {
            _mainMenuPanel.SetActive(false);
            _confirmationPanel.SetActive(true);
        }

        private void HandleCloseConfirmation(CloseConfirmationRequestedEvent e)
        {
            _confirmationPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        private void HandleNewGameConfirmed(NewGameConfirmedEvent e)
        {
            _confirmationPanel.SetActive(false);
        }

        // ─── Helpers ──────────────────────────────────────────────────
        private void ShowMainMenu()
        {
            _mainMenuPanel.SetActive(true);
            _optionsPanel.SetActive(false);
            _loadGamePanel.SetActive(false);
            _confirmationPanel.SetActive(false);
        }

        private void UpdateButtonVisibility()
        {
            var progress = SaveSystem.Load();
            bool showExtraButtons = progress.IntroductionCompleted;

            _newGameButton.SetActive(showExtraButtons);
            _loadGameButton.SetActive(showExtraButtons);
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _eventBus?.Unsubscribe<OpenOptionsRequestedEvent>(HandleOpenOptions);
            _eventBus?.Unsubscribe<CloseOptionsRequestedEvent>(HandleCloseOptions);
            _eventBus?.Unsubscribe<OpenLoadGameRequestedEvent>(HandleOpenLoadGame);
            _eventBus?.Unsubscribe<CloseLoadGameRequestedEvent>(HandleCloseLoadGame);
            _eventBus?.Unsubscribe<NewGameRequestedEvent>(HandleNewGameRequested);
            _eventBus?.Unsubscribe<CloseConfirmationRequestedEvent>(HandleCloseConfirmation);
            _eventBus?.Unsubscribe<NewGameConfirmedEvent>(HandleNewGameConfirmed);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}