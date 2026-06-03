using System;

namespace RollABall.Presentation.UI.HUD
{
    public class HudModel
    {
        private int _keys;
        private float _elapsedSeconds;
        private int _score;

        public int Keys             => _keys;
        public float ElapsedSeconds => _elapsedSeconds;
        public int Score            => _score;

        public event Action<int>   OnKeysChanged;
        public event Action<float> OnTimerChanged;
        public event Action<int>   OnScoreChanged;

        public void SetKeys(int keys)
        {
            _keys = keys;
            OnKeysChanged?.Invoke(_keys);
        }

        public void SetTimer(float elapsedSeconds)
        {
            _elapsedSeconds = elapsedSeconds;
            OnTimerChanged?.Invoke(_elapsedSeconds);
        }

        public void SetScore(int score)
        {
            _score = score;
            OnScoreChanged?.Invoke(_score);
        }

        public void Reset()
        {
            _keys           = 0;
            _elapsedSeconds = 0f;
            _score          = 0;

            OnKeysChanged?.Invoke(_keys);
            OnTimerChanged?.Invoke(_elapsedSeconds);
            OnScoreChanged?.Invoke(_score);
        }
    }
}