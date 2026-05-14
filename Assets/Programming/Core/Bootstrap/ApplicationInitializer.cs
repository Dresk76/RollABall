using RollABall.Infrastructure.Configuration;
using UnityEngine;

namespace RollABall.Core.Bootstrap
{
    public class ApplicationInitializer : MonoBehaviour
    {
        [SerializeField, Tooltip("Configuración general de la aplicación")]
        private ApplicationSettings _applicationSettings;

        private void OnValidate()
        {
            Debug.Assert(_applicationSettings != null, nameof(_applicationSettings));
        }

        private void Awake()
        {
            Application.targetFrameRate = _applicationSettings.TargetFrameRate;
        }
    }
}