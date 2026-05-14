
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
        private static GameManager _instance;
        private IEventBus _eventBus;
        private GameModel _gameModel;

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
            _gameModel = new GameModel();

            _eventBus.Subscribe<StartGameRequestedEvent>(HandleStartGameRequested);
            // _eventBus.Subscribe<GameOverRequestedEvent>(HandleGameOverRequested);
            // _eventBus.Subscribe<PauseRequestedEvent>(HandlePauseRequested);
        }

        // ─── Handlers ───────────────────────────────────────────────

        private void HandleStartGameRequested(StartGameRequestedEvent e)
        {
            _gameModel.Reset();
            SetGameState(GameState.Playing);
            _eventBus.Publish(new LoadSceneEvent(SceneType.IntroductionLevelScene));
        }

        // private void HandleGameOverRequested(GameOverRequestedEvent e)
        // {
        //     SetGameState(GameState.GameOver);
        // }

        // private void HandlePauseRequested(PauseRequestedEvent e)
        // {
        //     GameState newState = e.IsPausing ? GameState.Paused : GameState.Playing;
        //     SetGameState(newState);
        // }

        // ─── API pública ─────────────────────────────────────────────

        public void AddKeys(int amount) => _gameModel.AddKeys(amount);
        public void AddScore(int amount) => _gameModel.AddScore(amount);

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
            // _eventBus?.Unsubscribe<GameOverRequestedEvent>(HandleGameOverRequested);
            // _eventBus?.Unsubscribe<PauseRequestedEvent>(HandlePauseRequested);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}