using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "Application Settings", menuName = "Scriptable Objects/Config/Application Settings")]
    public class ApplicationSettings : ScriptableObject
    {
        [SerializeField, Tooltip("Fotogramas por segundo objetivo")]
        private int _targetFrameRate = 60;

        public int TargetFrameRate => _targetFrameRate;
    }
}