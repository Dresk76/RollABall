using RollABall.Core.Events;
using RollABall.Core.Interfaces;
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
            eventBus.Publish(new LevelLoadedEvent(
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
                _levelConfiguration.TotalKeys,  // ← nuevo
                _areaVictory
            );
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
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