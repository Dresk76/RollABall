using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "Level Configuration", menuName = "Scriptable Objects/Config/Level Configuration")]
    public class LevelConfiguration : ScriptableObject
    {
        [SerializeField, Tooltip("Nombre del nivel")]
        private string _levelName = "Level";

        [SerializeField, Tooltip("Total de llaves en el nivel")]
        private int _totalKeys;

        [SerializeField, Tooltip("Score máximo posible en el nivel")]
        private int _maxScore = 1000;

        public string LevelName => _levelName;
        public int TotalKeys    => _totalKeys;
        public int MaxScore     => _maxScore;
    }
}