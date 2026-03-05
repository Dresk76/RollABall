using RollABall.Programming.Core.Events;
using RollABall.Programming.Core.Interfaces;
using RollABall.Programming.Data.Enums;
using System;
using UnityEngine;

namespace RollABall.Programming.Core.Managers
{
    public sealed class GameManager : MonoBehaviour, IGlobalInitializable, IDisposable
    {
        private static GameManager _instance;
        private IEventBus _eventBus;

        // Keys
        [SerializeField] private int _keys;


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
            _eventBus.Subscribe<StartGameRequestedEvent>(HandleStartGameRequested);
        }

        private void HandleStartGameRequested(StartGameRequestedEvent e)
        {
            StartGame();
        }

        private void StartGame()
        {
            _keys = 0;
            SetGameState(GameState.Playing);

            _eventBus.Publish(new LoadSceneEvent(SceneType.IntroductionLevelScene));
        }

        private void SetGameState(GameState newState)
        {
            _eventBus.Publish(new GameStateChangedEvent(newState));
        }

        private void GameOver()
        {
            SetGameState(GameState.GameOver);
        }


        public void AddKeys(int amount)
        {
            _keys += amount;
        }

        public void Dispose()
        {
            _eventBus?.Unsubscribe<StartGameRequestedEvent>(HandleStartGameRequested);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
