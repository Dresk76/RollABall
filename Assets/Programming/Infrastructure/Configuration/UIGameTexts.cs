using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "UI Game Texts", menuName = "Scriptable Objects/Config/UI Game Texts")]
    public class UIGameTexts : ScriptableObject
    {
        [Header("PAUSE")]
        [SerializeField] private string _pauseTitleText      = "PAUSE";
        [SerializeField] private string _currentLevelPrefix  = "LEVEL: ";
        [SerializeField] private string _timerPrefix         = "TIME: ";
        [SerializeField] private string _collectedKeysPrefix = "KEYS: ";

        [Header("PAUSE BUTTONS")]
        [SerializeField] private string _resumeText   = "RESUME";
        [SerializeField] private string _restartText  = "RESTART";
        [SerializeField] private string _optionsText  = "OPTIONS";
        [SerializeField] private string _mainMenuText = "MAIN MENU";

        public string PauseTitleText      => _pauseTitleText;
        public string CurrentLevelPrefix  => _currentLevelPrefix;
        public string TimerPrefix         => _timerPrefix;
        public string CollectedKeysPrefix => _collectedKeysPrefix;
        public string ResumeText          => _resumeText;
        public string RestartText         => _restartText;
        public string OptionsText         => _optionsText;
        public string MainMenuText        => _mainMenuText;
    }
}