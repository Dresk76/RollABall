using RollABall.Core.Events;
using System;
using UnityEngine;

namespace RollABall.Domain.Gameplay.Keys
{
    public class Key : MonoBehaviour
    {
        [SerializeField, Tooltip("Tiempo antes de destruirse al ser recogida")]
        private float _harvestTime = 0.1f;

        private IEventBus _eventBus;

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        private void OnTriggerEnter(Collider other)
        {
            _eventBus.Publish(new KeyCollectedEvent());   // ← sin el 1
            Destroy(gameObject, _harvestTime);
        }
    }
}