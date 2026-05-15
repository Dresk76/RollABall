using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RollABall.Core.Bootstrap
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        private static Bootstrapper _instance;
        private IEventBus _eventBus;

        [Header("Global Systems")]
        [SerializeField] private MonoBehaviour[] _globalSystems;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            _eventBus = new EventBus();

            InitializeGlobalSystems();

            SceneManager.LoadScene(1); // índice 1 = 01_intro en Build Settings

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void InitializeGlobalSystems()
        {
            foreach (var system in _globalSystems)
            {
                if (system is IGlobalInitializable initializable)
                {
                    initializable.Initialize(_eventBus);
                }
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InitializeSceneSystems();
        }

        private void InitializeSceneSystems()
        {
            MonoBehaviour[] sceneSystems = FindObjectsOfType<MonoBehaviour>(true);

            foreach (var system in sceneSystems)
            {
                if (system is ISceneInitializable initializable)
                {
                    initializable.Initialize(_eventBus);
                }
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}