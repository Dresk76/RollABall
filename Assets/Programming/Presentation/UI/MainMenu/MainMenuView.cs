using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace RollABall.Presentation.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("BUTTONS")]
        [SerializeField] private Button _startGameButton;
        public Button StartGameButton => _startGameButton;
        [SerializeField] private Button _optionsGameButton;
        public Button OptionsGameButton => _optionsGameButton;
        
        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _playText;
        public TextMeshProUGUI PlayText => _playText;
        [SerializeField] private TextMeshProUGUI _optionsText;
        public TextMeshProUGUI OptionsText => _optionsText;

        private void OnValidate()
        {
            Debug.Assert(StartGameButton != null, nameof(StartGameButton));
            Debug.Assert(OptionsGameButton != null, nameof(OptionsGameButton));
            Debug.Assert(PlayText != null, nameof(PlayText));
            Debug.Assert(OptionsText != null, nameof(OptionsText));
        }

        public void SetTextColor(Color color)
        {
            _playText.color = color;
            _optionsText.color = color;
        }
    }
}
