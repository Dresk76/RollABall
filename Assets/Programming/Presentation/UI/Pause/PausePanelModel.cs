using System;

namespace RollABall.Presentation.UI.Pause
{
    public class PausePanelModel
    {
        private string _levelName;
        private float _elapsedSeconds;
        private int _collectedKeys;

        public string LevelName       => _levelName;
        public float ElapsedSeconds   => _elapsedSeconds;
        public int CollectedKeys      => _collectedKeys;

        public event Action<string> OnLevelNameChanged;
        public event Action<float>  OnTimerChanged;
        public event Action<int>    OnKeysChanged;

        public void SetLevelName(string levelName)
        {
            _levelName = levelName;
            OnLevelNameChanged?.Invoke(_levelName);
        }

        public void SetTimer(float elapsedSeconds)
        {
            _elapsedSeconds = elapsedSeconds;
            OnTimerChanged?.Invoke(_elapsedSeconds);
        }

        public void SetKeys(int keys)
        {
            _collectedKeys = keys;
            OnKeysChanged?.Invoke(_collectedKeys);
        }
    }
}