using RollABall.Infrastructure.Configuration;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.Pause
{
    public class PausePanelView : MonoBehaviour
    {
        [Header("INFO")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _collectedKeysText;

        [Header("BUTTONS")]
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _mainMenuButton;

        [Header("CONFIGURATION")]
        [SerializeField] private UIGameTexts _texts;

        public Button ResumeButton   => _resumeButton;
        public Button RestartButton  => _restartButton;
        public Button OptionsButton  => _optionsButton;
        public Button MainMenuButton => _mainMenuButton;

        private void OnValidate()
        {
            Debug.Assert(_titleText != null, nameof(_titleText));
            Debug.Assert(_currentLevelText != null, nameof(_currentLevelText));
            Debug.Assert(_timerText != null, nameof(_timerText));
            Debug.Assert(_collectedKeysText != null, nameof(_collectedKeysText));
            Debug.Assert(_resumeButton != null, nameof(_resumeButton));
            Debug.Assert(_restartButton != null, nameof(_restartButton));
            Debug.Assert(_optionsButton != null, nameof(_optionsButton));
            Debug.Assert(_mainMenuButton != null, nameof(_mainMenuButton));
            Debug.Assert(_texts != null, nameof(_texts));
        }

        private void Awake()
        {
            _titleText.text = _texts.PauseTitleText;
        }

        public void SetLevelName(string levelName)
        {
            _currentLevelText.text = $"{_texts.CurrentLevelPrefix}{levelName}";
        }

        public void SetTimer(float elapsedSeconds)
        {
            TimeSpan time   = TimeSpan.FromSeconds(elapsedSeconds);
            _timerText.text = $"{_texts.TimerPrefix}{time.Minutes:00}:{time.Seconds:00}";
        }

        public void SetKeys(int keys)
        {
            _collectedKeysText.text = $"{_texts.CollectedKeysPrefix}{keys}";
        }
    }
}