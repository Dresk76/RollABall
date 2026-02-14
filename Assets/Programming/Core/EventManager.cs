using System;

namespace RollABall.Programming.Core
{
    public static class EventManager
    {
        public static event Action OnOpenTrapDoor;


        // Método que invoca el evento OnOpenTrapDoor, notificando a los suscriptores sobre la trampa abierta.
        public static void OpenTrapDoor()
        {
            OnOpenTrapDoor?.Invoke();
        }
    }
}
