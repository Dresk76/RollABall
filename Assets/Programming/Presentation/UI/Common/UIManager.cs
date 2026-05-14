using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using System;
using UnityEngine;

namespace RollABall.Presentation.UI.Common
{
    public class UIManager : MonoBehaviour, ISceneInitializable, IDisposable
    {
        private IEventBus _eventBus;

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventBus.Subscribe<GameStateChangedEvent>(HandleGameStateChanged);
        }

        private void HandleGameStateChanged(GameStateChangedEvent e)
        {
            // aquí irá la lógica de mostrar/ocultar paneles
        }

        public void Dispose()
        {
            _eventBus?.Unsubscribe<GameStateChangedEvent>(HandleGameStateChanged);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}