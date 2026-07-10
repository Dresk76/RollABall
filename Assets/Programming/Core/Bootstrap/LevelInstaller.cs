using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Domain.Enums;
using RollABall.Domain.Gameplay.Environment;
using RollABall.Domain.Gameplay.Keys;
using RollABall.Domain.Gameplay.Level;
using RollABall.Infrastructure.Configuration;
using UnityEngine;

namespace RollABall.Core.Bootstrap
{
    public class LevelInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private Key[] _keys;
        [SerializeField] private TrapDoor _trapDoor;
        [SerializeField] private AreaReloadCurrentScene _areaReload;
        [SerializeField] private AreaVictory _areaVictory;

        [SerializeField, Tooltip("Configuración única de este nivel")]
        private LevelConfiguration _levelConfiguration;

        private LevelController _levelController;
        private IEventBus _eventBus;
        private bool _levelReady;
        private bool _playingPublished;

        private void OnValidate()
        {
            Debug.Assert(_keys != null, nameof(_keys));
            Debug.Assert(_trapDoor != null, nameof(_trapDoor));
            Debug.Assert(_areaReload != null, nameof(_areaReload));
            Debug.Assert(_areaVictory != null, nameof(_areaVictory));
            Debug.Assert(_levelConfiguration != null, nameof(_levelConfiguration));
        }

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Publish(new LevelLoadedEvent(
                _levelConfiguration.TotalKeys,
                _levelConfiguration.MaxScore,
                _levelConfiguration.LevelName
            ));

            foreach (Key key in _keys)
            {
                key.Initialize(eventBus);
            }

            _trapDoor.Initialize(eventBus);
            _areaReload.Initialize(eventBus);
            _areaVictory.Initialize(eventBus);

            _levelController = new LevelController(
                eventBus,
                _levelConfiguration.MaxScore,
                _levelConfiguration.TotalKeys,
                _areaVictory
            );

            _levelReady = true;
        }

        private void Update()
        {
            if (_levelReady && !_playingPublished)
            {
                _playingPublished = true;
                Time.timeScale = 1f;   // ← garantiza tiempo normal al iniciar el nivel
                _eventBus.Publish(new GameStateChangedEvent(GameState.Playing));
            }

            _levelController?.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _levelController?.Dispose();
        }
    }
}