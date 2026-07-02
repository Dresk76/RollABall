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
        // ─── Campos ───────────────────────────────────────────────────
        [SerializeField] private Key[] _keys;
        [SerializeField] private TrapDoor _trapDoor;
        [SerializeField] private AreaReloadCurrentScene _areaReload;
        [SerializeField] private AreaVictory _areaVictory;

        [SerializeField, Tooltip("Configuración única de este nivel")]
        private LevelConfiguration _levelConfiguration;

        private LevelController _levelController;
        private IEventBus _eventBus;
        private bool _levelReady;

        // ─── Validación ───────────────────────────────────────────────
        private void OnValidate()
        {
            Debug.Assert(_keys != null, nameof(_keys));
            Debug.Assert(_trapDoor != null, nameof(_trapDoor));
            Debug.Assert(_areaReload != null, nameof(_areaReload));
            Debug.Assert(_areaVictory != null, nameof(_areaVictory));
            Debug.Assert(_levelConfiguration != null, nameof(_levelConfiguration));
        }

        // ─── Inicialización ───────────────────────────────────────────
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

        // ─── Ciclo de vida ────────────────────────────────────────────
        private void Start()
        {
            // Se ejecuta después de que TODOS los Initialize de la escena
            // terminaron, así el BallController ya está suscrito y recibe
            // el estado Playing sin perderlo.
            if (_levelReady)
            {
                _eventBus.Publish(new GameStateChangedEvent(GameState.Playing));
            }
        }

        private void Update()
        {
            _levelController?.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _levelController?.Dispose();
        }
    }
}