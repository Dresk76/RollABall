using UnityEngine;

namespace RollABall.Programming.Settings
{
    public class ApplicationInitializer : MonoBehaviour
    {
        [Tooltip("Set the desired framerate")]
        [SerializeField] private ApplicationSettings applicationSettings;


        private void Awake()
        {
            Application.targetFrameRate = applicationSettings.targetFrameRate;
        }
    }
}
