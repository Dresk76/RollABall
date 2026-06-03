using RollABall.Infrastructure.Configuration;
using TMPro;
using System;
using UnityEngine;

namespace RollABall.Presentation.UI.HUD
{
    public class HudView : MonoBehaviour
    {
        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _keysText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _timerText;

        [Header("CONFIGURATION")]
        [SerializeField] private UIHudTexts _texts;

        private void OnValidate()
        {
            Debug.Assert(_keysText != null, nameof(_keysText));
            Debug.Assert(_scoreText != null, nameof(_scoreText));
            Debug.Assert(_timerText != null, nameof(_timerText));
            Debug.Assert(_texts != null, nameof(_texts));
        }

        public void SetKeys(int keys)
        {
            _keysText.text = $"{_texts.KeysPrefix}{keys}";
        }

        public void SetTimer(float elapsedSeconds)
        {
            TimeSpan time   = TimeSpan.FromSeconds(elapsedSeconds);
            _timerText.text = $"{_texts.TimerPrefix}{time.Minutes:00}:{time.Seconds:00}";
        }

        public void SetScore(int score)
        {
            _scoreText.text = $"{_texts.ScorePrefix}{score}";
        }
    }
}