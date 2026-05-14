using System;

namespace RollABall.Domain.Models
{
    public class GameModel
    {
        private int _keys;
        private int _score;

        public int Keys => _keys;
        public int Score => _score;

        public event Action<int> OnKeysChanged;
        public event Action<int> OnScoreChanged;

        public void AddKeys(int amount)
        {
            if (amount <= 0) return;

            _keys += amount;
            OnKeysChanged?.Invoke(_keys);
        }

        public void AddScore(int amount)
        {
            if (amount <= 0) return;

            _score += amount;
            OnScoreChanged?.Invoke(_score);
        }

        public void Reset()
        {
            _keys = 0;
            _score = 0;

            OnKeysChanged?.Invoke(_keys);
            OnScoreChanged?.Invoke(_score);
        }
    }
}