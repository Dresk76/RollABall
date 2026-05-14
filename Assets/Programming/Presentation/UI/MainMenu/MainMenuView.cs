using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("BUTTONS")]
        [SerializeField] private Button _startGameButton;
        public Button StartGameButton => _startGameButton;

        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _playButtonText;

        private const string NewGameText = "Jugar";
        private const string ContinueText = "Continuar";

        private void OnValidate()
        {
            Debug.Assert(_startGameButton != null, nameof(_startGameButton));
            Debug.Assert(_playButtonText != null, nameof(_playButtonText));
        }

        public void SetPlayButtonText(bool hasActiveGame)
        {
            _playButtonText.text = hasActiveGame ? ContinueText : NewGameText;
        }
    }
}