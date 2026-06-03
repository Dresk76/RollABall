using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.Victory
{
    public class VictoryPanelView : MonoBehaviour
    {
        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _keysText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        [Header("BUTTONS")]
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartButton;

        [Header("EFFECTS")]
        [SerializeField] private ConfettiController _confetti;

        public Button NextLevelButton => _nextLevelButton;
        public Button MainMenuButton  => _mainMenuButton;
        public Button RestartButton   => _restartButton;

        private void OnValidate()
        {
            Debug.Assert(_keysText != null, nameof(_keysText));
            Debug.Assert(_timeText != null, nameof(_timeText));
            Debug.Assert(_scoreText != null, nameof(_scoreText));
            Debug.Assert(_nextLevelButton != null, nameof(_nextLevelButton));
            Debug.Assert(_mainMenuButton != null, nameof(_mainMenuButton));
            Debug.Assert(_restartButton != null, nameof(_restartButton));
            Debug.Assert(_confetti != null, nameof(_confetti));
        }

        public void Show(int keys, float elapsedSeconds, int score)
        {
            TimeSpan time   = TimeSpan.FromSeconds(elapsedSeconds);

            _keysText.text  = $"Keys: {keys}";
            _timeText.text  = $"Time: {time.Minutes:00}:{time.Seconds:00}";
            _scoreText.text = $"Score: {score}";

            _confetti.Play();
        }

        public void Hide()
        {
            _confetti.Stop();
        }
    }
}