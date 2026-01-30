using System;
using UnityEngine;

namespace RollABall.Programming.Core
{
    public static class EventManager
    {
        public static event Action<bool> OnOpenTrapDoor;
        public static event Action<int> OnKeyRecovered; 


        // Método que invoca el evento OnOpenTrapDoor, notificando a los suscriptores sobre la trampa abierta.
        public static void OpenTrapDoor(bool state)
        {
            OnOpenTrapDoor?.Invoke(state);
        }

        public static void KeyRecovered(int keyValue)
        {
            OnKeyRecovered?.Invoke(keyValue);
        }
    }
}
