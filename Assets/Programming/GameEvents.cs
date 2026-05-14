using System;

namespace RollABall.Programming.Core.Events
{
    public static class GameEvents
    {
        public static event Action OnTrapDoorOpened;
        public static event Action OnStartGameRequested;


        /// <summary>
        /// Métodos wrapper
        /// </summary>
        // Método que invoca el evento OnOpenTrapDoor, notificando a los suscriptores sobre la trampa abierta.
        public static void RaiseTrapDoorOpened()
        {
            OnTrapDoorOpened?.Invoke();
        }

        public static void RaiseStartGameRequested()
        {
            OnStartGameRequested?.Invoke();
        }
    }
}
