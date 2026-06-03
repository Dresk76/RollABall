using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RollABall.Presentation.UI.Buttons
{
    public class UIHoverable : MonoBehaviour, 
        IPointerEnterHandler, 
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler
    {
        public event Action HoverEntered;
        public event Action HoverExited;
        public event Action PointerPressed;
        public event Action PointerReleased;

        public void OnPointerEnter(PointerEventData eventData)
        {
            HoverEntered?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HoverExited?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerPressed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerReleased?.Invoke();
        }
    }
}