using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "UI Main Menu Texts", menuName = "Scriptable Objects/Config/UI Main Menu Texts")]
    public class UIMainMenuTexts : ScriptableObject
    {
        [SerializeField] private string _newGameText = "PLAY";
        [SerializeField] private string _continueText = "CONTINUE";
        [SerializeField] private string _optionsText = "OPTIONS";
        [SerializeField] private string _quitText = "QUIT";

        public string NewGameText => _newGameText;
        public string ContinueText => _continueText;
        public string OptionsText => _optionsText;
        public string QuitText => _quitText;
    }
}