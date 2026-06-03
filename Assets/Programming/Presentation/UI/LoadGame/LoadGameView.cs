using RollABall.Infrastructure.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.LoadGame
{
    public class LoadGameView : MonoBehaviour
    {
        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _titleText;

        [Header("CAROUSEL")]
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;

        [Header("LEVEL ENTRY")]
        [SerializeField] private LevelEntryView _levelEntry;

        [Header("NAVIGATION")]
        [SerializeField] private Button _backButton;

        [Header("CONFIGURATION")]
        [SerializeField] private UIMainMenuTexts _texts;

        public Button PrevButton    => _prevButton;
        public Button NextButton    => _nextButton;
        public Button BackButton    => _backButton;
        public LevelEntryView LevelEntry => _levelEntry;

        private int _currentIndex = 0;
        private int _totalLevels  = 0;

        private void OnValidate()
        {
            Debug.Assert(_titleText != null, nameof(_titleText));
            Debug.Assert(_prevButton != null, nameof(_prevButton));
            Debug.Assert(_nextButton != null, nameof(_nextButton));
            Debug.Assert(_levelEntry != null, nameof(_levelEntry));
            Debug.Assert(_backButton != null, nameof(_backButton));
            Debug.Assert(_texts != null, nameof(_texts));
        }

        private void Awake()
        {
            _titleText.text = _texts.LoadGameTitleText;
        }

        public void Setup(int totalLevels)
        {
            _totalLevels = totalLevels;
            ResetCarousel();
        }

        public int GetCurrentIndex() => _currentIndex;

        public void ScrollToNext()
        {
            if (_currentIndex >= _totalLevels - 1) return;
            _currentIndex++;
            UpdateNavigationButtons();
        }

        public void ScrollToPrev()
        {
            if (_currentIndex <= 0) return;
            _currentIndex--;
            UpdateNavigationButtons();
        }

        public void ResetCarousel()
        {
            _currentIndex = 0;
            UpdateNavigationButtons();
        }

        private void UpdateNavigationButtons()
        {
            _prevButton.interactable = _currentIndex > 0;
            _nextButton.interactable = _currentIndex < _totalLevels - 1;
        }
    }
}