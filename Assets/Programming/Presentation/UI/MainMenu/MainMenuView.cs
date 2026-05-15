using RollABall.Infrastructure.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("BUTTONS")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _quitGameButton;

        public Button StartGameButton => _startGameButton;
        public Button QuitGameButton => _quitGameButton;

        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _playButtonText;
        [SerializeField] private TextMeshProUGUI _optionsButtonText;
        [SerializeField] private TextMeshProUGUI _quitButtonText;

        [Header("CONFIGURATION")]
        [SerializeField] private UIMainMenuTexts _texts;

        private void OnValidate()
        {
            Debug.Assert(_startGameButton != null, nameof(_startGameButton));
            Debug.Assert(_quitGameButton != null, nameof(_quitGameButton));
            Debug.Assert(_playButtonText != null, nameof(_playButtonText));
            Debug.Assert(_optionsButtonText != null, nameof(_optionsButtonText));
            Debug.Assert(_quitButtonText != null, nameof(_quitButtonText));
            Debug.Assert(_texts != null, nameof(_texts));
        }

        private void Start()
        {
            // Aplica los textos desde el ScriptableObject al arrancar
            _optionsButtonText.text = _texts.OptionsText;
            _quitButtonText.text = _texts.QuitText;
            _playButtonText.text = _texts.NewGameText;
        }

        public void SetPlayButtonText(bool hasActiveGame)
        {
            _playButtonText.text = hasActiveGame ? _texts.ContinueText : _texts.NewGameText;
        }
    }
}