using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "UI HUD Texts", menuName = "Scriptable Objects/Config/UI HUD Texts")]
    public class UIHudTexts : ScriptableObject
    {
        [SerializeField] private string _keysPrefix = "KEYS: ";
        [SerializeField] private string _scorePrefix = "SCORE: ";
        [SerializeField] private string _timerPrefix = "TIME: ";

        public string KeysPrefix => _keysPrefix;
        public string ScorePrefix => _scorePrefix;
        public string TimerPrefix => _timerPrefix;
    }
}