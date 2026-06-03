using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Domain.Enums;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.HUD
{
    public class PauseInputHandler : MonoBehaviour, ISceneInitializable, IDisposable
    {
        [SerializeField] private Button _pauseButton;

        private IEventBus _eventBus;
        private bool _isPaused    = false;
        private bool _isInOptions = false;

        private void OnValidate()
        {
            Debug.Assert(_pauseButton != null, nameof(_pauseButton));
        }

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _pauseButton.onClick.AddListener(TogglePause);

            _eventBus.Subscribe<GameStateChangedEvent>(HandleGameStateChanged);
            _eventBus.Subscribe<OpenOptionsRequestedEvent>(HandleOpenOptions);
            _eventBus.Subscribe<CloseOptionsRequestedEvent>(HandleCloseOptions);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (_isInOptions)
                _eventBus.Publish(new CloseOptionsRequestedEvent());
            else if (_isPaused || !_isPaused)
                TogglePause();
        }

        private void TogglePause()
        {
            _eventBus.Publish(new PauseRequestedEvent(!_isPaused));
        }

        private void HandleGameStateChanged(GameStateChangedEvent e)
        {
            _isPaused = e.NewState == GameState.Paused;
        }

        private void HandleOpenOptions(OpenOptionsRequestedEvent e)
        {
            _isInOptions = true;
        }

        private void HandleCloseOptions(CloseOptionsRequestedEvent e)
        {
            _isInOptions = false;
        }

        public void Dispose()
        {
            _pauseButton.onClick.RemoveListener(TogglePause);
            _eventBus?.Unsubscribe<GameStateChangedEvent>(HandleGameStateChanged);
            _eventBus?.Unsubscribe<OpenOptionsRequestedEvent>(HandleOpenOptions);
            _eventBus?.Unsubscribe<CloseOptionsRequestedEvent>(HandleCloseOptions);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}