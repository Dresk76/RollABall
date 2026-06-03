using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "UI Main Menu Texts", menuName = "Scriptable Objects/Config/UI Main Menu Texts")]
    public class UIMainMenuTexts : ScriptableObject
    {
        [Header("PLAY BUTTON")]
        [SerializeField] private string _newGameText    = "PLAY";
        [SerializeField] private string _continueText   = "CONTINUE";

        [Header("MAIN MENU BUTTONS")]
        [SerializeField] private string _optionsText    = "OPTIONS";
        [SerializeField] private string _newGameBtnText = "NEW GAME";
        [SerializeField] private string _loadGameText   = "LOAD GAME";
        [SerializeField] private string _quitText       = "QUIT";

        [Header("OPTIONS")]
        [SerializeField] private string _optionsTitleText = "OPTIONS";
        [SerializeField] private string _musicLabelText   = "MUSIC";
        [SerializeField] private string _sfxLabelText     = "SFX";
        [SerializeField] private string _backText         = "BACK";

        [Header("LOAD GAME")]
        [SerializeField] private string _loadGameTitleText = "LOAD GAME";

        [Header("CONFIRMATION")]
        [SerializeField] private string _confirmationTitleText   = "DELETE SAVE DATA?";
        [SerializeField] private string _confirmationMessageText = "ARE YOU SURE YOU WANT TO DELETE YOUR SAVE?";
        [SerializeField] private string _confirmText             = "YES, DELETE";
        [SerializeField] private string _cancelText              = "CANCEL";

        public string NewGameText          => _newGameText;
        public string ContinueText         => _continueText;
        public string OptionsText          => _optionsText;
        public string NewGameBtnText       => _newGameBtnText;
        public string LoadGameText         => _loadGameText;
        public string QuitText             => _quitText;
        public string OptionsTitleText     => _optionsTitleText;
        public string MusicLabelText       => _musicLabelText;
        public string SfxLabelText         => _sfxLabelText;
        public string BackText             => _backText;
        public string LoadGameTitleText    => _loadGameTitleText;
        public string ConfirmationTitleText   => _confirmationTitleText;
        public string ConfirmationMessageText => _confirmationMessageText;
        public string ConfirmText             => _confirmText;
        public string CancelText              => _cancelText;
    }
}