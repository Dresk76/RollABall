using RollABall.Infrastructure.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("BUTTONS")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _quitButton;

        public Button PlayButton     => _playButton;
        public Button NewGameButton  => _newGameButton;
        public Button LoadGameButton => _loadGameButton;
        public Button OptionsButton  => _optionsButton;
        public Button QuitButton     => _quitButton;

        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _playButtonText;
        [SerializeField] private TextMeshProUGUI _newGameButtonText;
        [SerializeField] private TextMeshProUGUI _loadGameButtonText;
        [SerializeField] private TextMeshProUGUI _optionsButtonText;
        [SerializeField] private TextMeshProUGUI _quitButtonText;

        [Header("CONFIGURATION")]
        [SerializeField] private UIMainMenuTexts _texts;

        private void OnValidate()
        {
            Debug.Assert(_playButton != null, nameof(_playButton));
            Debug.Assert(_newGameButton != null, nameof(_newGameButton));
            Debug.Assert(_loadGameButton != null, nameof(_loadGameButton));
            Debug.Assert(_optionsButton != null, nameof(_optionsButton));
            Debug.Assert(_quitButton != null, nameof(_quitButton));
            Debug.Assert(_playButtonText != null, nameof(_playButtonText));
            Debug.Assert(_newGameButtonText != null, nameof(_newGameButtonText));
            Debug.Assert(_loadGameButtonText != null, nameof(_loadGameButtonText));
            Debug.Assert(_optionsButtonText != null, nameof(_optionsButtonText));
            Debug.Assert(_quitButtonText != null, nameof(_quitButtonText));
            Debug.Assert(_texts != null, nameof(_texts));
        }

        private void Awake()
        {
            _newGameButtonText.text  = _texts.NewGameBtnText;
            _loadGameButtonText.text = _texts.LoadGameText;
            _optionsButtonText.text  = _texts.OptionsText;
            _quitButtonText.text     = _texts.QuitText;
        }

        public void SetPlayButtonText(bool hasActiveGame)
        {
            _playButtonText.text = hasActiveGame ? _texts.ContinueText : _texts.NewGameText;
        }
    }
}