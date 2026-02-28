using System;

namespace RollABall.Programming.UI.Intro
{
    public class IntroModel
    {
        private int _countdownValue;
        public event Action<int> OnCountdownChanged;


        public void SetCountdown(int value)
        {
            if (_countdownValue == value) return;

            _countdownValue = value;
            OnCountdownChanged?.Invoke(_countdownValue);
        }
    }
}
