using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Domain.Enums;
using RollABall.Domain.Models;
using RollABall.Infrastructure.Configuration;
using RollABall.Infrastructure.Save;
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
        private PlayerProgress _playerProgress;
        private int _currentLevelIndex;
        private int _totalKeysCurrentLevel;

        [SerializeField] private LevelProgressionConfig _levelProgressionConfig;

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

            // Carga el progreso guardado
            _playerProgress = SaveSystem.Load();

            _eventBus.Subscribe<StartGameRequestedEvent>(HandleStartGameRequested);
            _eventBus.Subscribe<ResumeGameRequestedEvent>(HandleResumeGameRequested);
            _eventBus.Subscribe<GameOverRequestedEvent>(HandleGameOverRequested);
            _eventBus.Subscribe<PauseRequestedEvent>(HandlePauseRequested);
            _eventBus.Subscribe<KeyCollectedEvent>(HandleKeyCollected);
            _eventBus.Subscribe<LevelCompletedEvent>(HandleLevelCompleted);
            _eventBus.Subscribe<LevelLoadedEvent>(HandleLevelLoaded);
            _eventBus.Subscribe<LoadSceneEvent>(HandleLoadScene);
            _eventBus.Subscribe<QuitGameRequestedEvent>(HandleQuitGameRequested);
            _eventBus.Subscribe<NextLevelRequestedEvent>(HandleNextLevelRequested);
            _eventBus.Subscribe<RestartRequestedEvent>(HandleRestartRequested);
            _eventBus.Subscribe<NewGameConfirmedEvent>(HandleNewGameConfirmed);
            _eventBus.Subscribe<LoadSpecificLevelRequestedEvent>(HandleLoadSpecificLevel);
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleStartGameRequested(StartGameRequestedEvent e)
        {
            _gameModel.Reset();
            _currentLevelIndex = 0;
            _playerProgress.HasPlayedBefore = true;
            _playerProgress.LastCompletedLevel = -1; // -1 significa ninguno completado
            _playerProgress.NextLevelToPlay = 0;

            SaveSystem.Save(_playerProgress);

            SetGameState(GameState.Playing);
            _eventBus.Publish(new LoadSceneEvent(
                _levelProgressionConfig.GetFirstLevel()
            ));
        }

        private void HandleResumeGameRequested(ResumeGameRequestedEvent e)
        {
            // Lee el progreso fresco del disco
            _playerProgress = SaveSystem.Load();
            _currentLevelIndex = _playerProgress.NextLevelToPlay;

            if (_levelProgressionConfig.TryGetLevelByIndex(
                _currentLevelIndex, out SceneType scene))
            {
                SetGameState(GameState.Playing);
                _eventBus.Publish(new LoadSceneEvent(scene));
            }
            else
            {
                _eventBus.Publish(new LoadSceneEvent(SceneType.MainMenuScene));
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

            _playerProgress.LastCompletedLevel = _currentLevelIndex;
            _playerProgress.NextLevelToPlay    = _currentLevelIndex + 1;
            _playerProgress.TotalScore        += e.FinalScore;

            // Desbloquea el siguiente nivel
            int nextLevel = _currentLevelIndex + 1;
            if (!_playerProgress.UnlockedLevels.Contains(nextLevel))
            {
                _playerProgress.UnlockedLevels.Add(nextLevel);
            }

            // Marca introduction como completada
            if (_currentLevelIndex == 0)
            {
                _playerProgress.IntroductionCompleted = true;
            }

            SaveSystem.Save(_playerProgress);
            SetGameState(GameState.GameOver);
        }

        private void HandleNextLevelRequested(NextLevelRequestedEvent e)
        {
            if (_levelProgressionConfig.TryGetNextLevel(
                _currentLevelIndex, out SceneType nextScene))
            {
                _currentLevelIndex++;
                SetGameState(GameState.Playing);
                _eventBus.Publish(new LoadSceneEvent(nextScene));
            }
            else
            {
                // No hay más niveles → vuelve al MainMenu
                _eventBus.Publish(new LoadSceneEvent(SceneType.MainMenuScene));
            }
        }

        private void HandleRestartRequested(RestartRequestedEvent e)
        {
            Time.timeScale = 1f;
            _gameModel.Reset();
            SetGameState(GameState.Playing);
            _eventBus.Publish(new FallRestartEvent());
        }

        private void HandleGameOverRequested(GameOverRequestedEvent e)
        {
            SetGameState(GameState.GameOver);
        }

        private void HandlePauseRequested(PauseRequestedEvent e)
        {
            GameState newState = e.IsPausing ? GameState.Paused : GameState.Playing;
            Time.timeScale     = e.IsPausing ? 0f : 1f;
            SetGameState(newState);
        }

        private void HandleLoadScene(LoadSceneEvent e)
        {
            if (e.SceneToLoad == SceneType.MainMenuScene)
            {
                // PLAY si nunca ha jugado, CONTINUE si tiene progreso
                bool hasActiveGame = _playerProgress.HasPlayedBefore &&
                                    _playerProgress.NextLevelToPlay > 0;

                _eventBus.Publish(new GameReadyEvent(hasActiveGame));
            }
        }

        private void HandleQuitGameRequested(QuitGameRequestedEvent e)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        private void HandleNewGameConfirmed(NewGameConfirmedEvent e)
        {
            // Resetea todo el progreso
            _playerProgress                      = new PlayerProgress();
            _playerProgress.HasPlayedBefore      = true;
            _playerProgress.IntroductionCompleted = false;
            _playerProgress.UnlockedLevels.Clear();
            _playerProgress.UnlockedLevels.Add(0); // solo desbloquea nivel 0

            SaveSystem.Save(_playerProgress);

            _gameModel.Reset();
            _currentLevelIndex = 0;

            SetGameState(GameState.Playing);
            _eventBus.Publish(new LoadSceneEvent(
                _levelProgressionConfig.GetFirstLevel()
            ));
        }

        private void HandleLoadSpecificLevel(LoadSpecificLevelRequestedEvent e)
        {
            if (_levelProgressionConfig.TryGetLevelByIndex(e.LevelIndex, out SceneType scene))
            {
                _currentLevelIndex = e.LevelIndex;
                SetGameState(GameState.Playing);
                _eventBus.Publish(new LoadSceneEvent(scene));
            }
        }

        // ─── API pública ──────────────────────────────────────────────
        public GameModel GetModel() => _gameModel;
        public PlayerProgress GetProgress() => _playerProgress;

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
            _eventBus?.Unsubscribe<LevelLoadedEvent>(HandleLevelLoaded);
            _eventBus?.Unsubscribe<LoadSceneEvent>(HandleLoadScene);
            _eventBus?.Unsubscribe<QuitGameRequestedEvent>(HandleQuitGameRequested);
            _eventBus?.Unsubscribe<NextLevelRequestedEvent>(HandleNextLevelRequested);
            _eventBus?.Unsubscribe<RestartRequestedEvent>(HandleRestartRequested);
            _eventBus?.Unsubscribe<NewGameConfirmedEvent>(HandleNewGameConfirmed);
            _eventBus?.Unsubscribe<LoadSpecificLevelRequestedEvent>(HandleLoadSpecificLevel);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}