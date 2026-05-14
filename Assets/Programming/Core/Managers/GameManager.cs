using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Domain.Enums;
using RollABall.Domain.Models;
using System;
using UnityEngine;

namespace RollABall.Core.Managers
{
    public sealed class GameManager : MonoBehaviour, IGlobalInitializable, IDisposable
    {
        // ─── Singleton ────────────────────────────────────────────────
        private static GameManager _instance;

        // ─── Campos ───────────────────────────────────────────────────
        private IEventBus _eventBus;
        private GameModel _gameModel;
        private bool _hasActiveGame;
        private int _totalKeysCurrentLevel;

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
            _gameModel = new GameModel();

            _eventBus.Subscribe<StartGameRequestedEvent>(HandleStartGameRequested);
            _eventBus.Subscribe<ResumeGameRequestedEvent>(HandleResumeGameRequested);
            _eventBus.Subscribe<GameOverRequestedEvent>(HandleGameOverRequested);
            _eventBus.Subscribe<PauseRequestedEvent>(HandlePauseRequested);
            _eventBus.Subscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus.Subscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus.Subscribe<LoadSceneEvent>(HandleLoadScene);
            _eventBus.Subscribe<LevelLoadedEvent>(HandleLevelLoaded);
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleStartGameRequested(StartGameRequestedEvent e)
        {
            _gameModel.Reset();
            _hasActiveGame = true;
            SetGameState(GameState.Playing);
            _eventBus.Publish(new LoadSceneEvent(SceneType.IntroductionLevelScene));
        }

        private void HandleResumeGameRequested(ResumeGameRequestedEvent e)
        {
            if (_hasActiveGame)
            {
                SetGameState(GameState.Playing);
                _eventBus.Publish(new LoadSceneEvent(SceneType.IntroductionLevelScene));
            }
            else
            {
                HandleStartGameRequested(new StartGameRequestedEvent());
            }
        }

        private void HandleLevelLoaded(LevelLoadedEvent e)
        {
            _totalKeysCurrentLevel = e.TotalKeys;
            _gameModel.Reset();
        }

        private void HandleKeyCollected(KeyCollectedEvent e)
        {
            _gameModel.AddKeys(e.KeyValue);

            if (_gameModel.Keys >= _totalKeysCurrentLevel)
            {
                _eventBus.Publish(new TrapDoorOpenEvent());
            }
        }

        private void HandleLevelCompleted(LevelCompletedEvent e)
        {
            _gameModel.SetScore(e.FinalScore);
            _hasActiveGame = false;
            SetGameState(GameState.GameOver);
        }

        private void HandleGameOverRequested(GameOverRequestedEvent e)
        {
            _hasActiveGame = false;
            SetGameState(GameState.GameOver);
        }

        private void HandlePauseRequested(PauseRequestedEvent e)
        {
            GameState newState = e.IsPausing ? GameState.Paused : GameState.Playing;
            SetGameState(newState);
        }

        private void HandleLoadScene(LoadSceneEvent e)
        {
            if (e.SceneToLoad == SceneType.MainMenuScene)
            {
                _eventBus.Publish(new GameReadyEvent(_hasActiveGame));
            }
        }

        // ─── API pública ──────────────────────────────────────────────
        public GameModel GetModel() => _gameModel;

        // ─── Helpers privados ─────────────────────────────────────────
        private void SetGameState(GameState newState)
        {
            _eventBus.Publish(new GameStateChangedEvent(newState));
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _eventBus?.Unsubscribe<StartGameRequestedEvent>(HandleStartGameRequested);
            _eventBus?.Unsubscribe<ResumeGameRequestedEvent>(HandleResumeGameRequested);
            _eventBus?.Unsubscribe<GameOverRequestedEvent>(HandleGameOverRequested);
            _eventBus?.Unsubscribe<PauseRequestedEvent>(HandlePauseRequested);
            _eventBus?.Unsubscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus?.Unsubscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus?.Unsubscribe<LoadSceneEvent>(HandleLoadScene);
            _eventBus?.Unsubscribe<LevelLoadedEvent>(HandleLevelLoaded);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}