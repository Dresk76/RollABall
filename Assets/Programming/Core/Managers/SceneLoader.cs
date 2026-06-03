using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Infrastructure.Configuration;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RollABall.Core.Managers
{
    public sealed class SceneLoader : MonoBehaviour, IGlobalInitializable, IDisposable
    {
        // ─── Singleton ────────────────────────────────────────────────
        private static SceneLoader _instance;

        // ─── Campos ───────────────────────────────────────────────────
        private IEventBus _eventBus;

        [SerializeField] private SceneConfiguration _sceneConfiguration;

        // ─── Ciclo de vida Unity ──────────────────────────────────────
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

        // ─── Inicialización ───────────────────────────────────────────
        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventBus.Subscribe<LoadSceneEvent>(HandleLoadSceneRequested);
            _eventBus.Subscribe<FallRestartEvent>(HandleFallRestart);
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleLoadSceneRequested(LoadSceneEvent e)
        {
            if (!_sceneConfiguration.TryGetSceneName(e.SceneToLoad, out string sceneName))
            {
                Debug.LogError($"No se encontró el mapeo de escena para {e.SceneToLoad}");
                return;
            }

            LoadScene(sceneName);
        }

        private void HandleFallRestart(FallRestartEvent e)
        {
            ReloadCurrentScene();
        }

        // ─── API pública ──────────────────────────────────────────────
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ReloadCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _eventBus?.Unsubscribe<LoadSceneEvent>(HandleLoadSceneRequested);
            _eventBus?.Unsubscribe<FallRestartEvent>(HandleFallRestart);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}