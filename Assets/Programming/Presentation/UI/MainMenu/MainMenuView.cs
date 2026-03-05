using UnityEngine.UI;
using UnityEngine;

namespace RollABall.Programming.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField, Tooltip("Boton para iniciar el juego")] private Button _startGameButton;
        public Button StartGameButton => _startGameButton;


        private void OnValidate()
        {
            Debug.Assert(StartGameButton != null, nameof(StartGameButton));
        }
    }
}
