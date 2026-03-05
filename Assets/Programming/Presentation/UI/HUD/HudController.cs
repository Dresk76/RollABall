using RollABall.Programming.Core.Events;
using RollABall.Programming.Data.Enums;
using UnityEngine;

namespace RollABall.Programming.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        private IEventBus _eventBus;

        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<GameStateChangedEvent>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedEvent e)
        {
            if (e.NewState == GameState.Playing)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
    }
}
