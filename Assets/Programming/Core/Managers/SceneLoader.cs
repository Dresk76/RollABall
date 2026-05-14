
using RollABall.Core.Interfaces;
using RollABall.Core.Events;
using RollABall.Infrastructure.Configuration;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;

namespace RollABall.Core.Managers
{
    public sealed class SceneLoader : MonoBehaviour, IGlobalInitializable, IDisposable
    {
        private static SceneLoader _instance;
        private IEventBus _eventBus;
        [SerializeField] private SceneConfiguration _sceneConfiguration;


        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventBus.Subscribe<LoadSceneEvent>(HandleLoadSceneRequested);
        }

        private void HandleLoadSceneRequested(LoadSceneEvent e)
        {
            if (!_sceneConfiguration.TryGetSceneName(e.SceneToLoad, out string sceneName))
            {
                Debug.LogError($"No se ha encontrado el mapeo de escena para {e.SceneToLoad}");
                return;
            }

            LoadScene(sceneName);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ReloadCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        public void Dispose()
        {
            _eventBus?.Unsubscribe<LoadSceneEvent>(HandleLoadSceneRequested);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
