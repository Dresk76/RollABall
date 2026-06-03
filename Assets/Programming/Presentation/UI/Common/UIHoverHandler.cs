using RollABall.Presentation.UI.Buttons;
using System;

namespace RollABall.Presentation.UI.Common
{
    public class UIHoverHandler : IDisposable
    {
        // ─── Estructura interna ───────────────────────────────────────
        private struct HoverActions
        {
            public Action OnEnter;
            public Action OnExit;
            public Action OnPressed;
            public Action OnReleased;
        }

        // ─── Campos ───────────────────────────────────────────────────
        private readonly UIHoverableButton[] _buttons;
        private readonly HoverActions[] _hoverActions;

        // ─── Constructor ──────────────────────────────────────────────
        public UIHoverHandler(UIHoverableButton[] buttons)
        {
            _buttons      = buttons ?? throw new ArgumentNullException(nameof(buttons));
            _hoverActions = new HoverActions[_buttons.Length];

            for (int i = 0; i < _buttons.Length; i++)
            {
                UIHoverableButton button = _buttons[i];

                _hoverActions[i] = new HoverActions
                {
                    OnEnter    = () => OnHoverEntered(button),
                    OnExit     = () => OnHoverExited(button),
                    OnPressed  = () => OnPointerPressed(button),
                    OnReleased = () => OnPointerReleased(button)
                };

                button.Hoverable.HoverEntered    += _hoverActions[i].OnEnter;
                button.Hoverable.HoverExited     += _hoverActions[i].OnExit;
                button.Hoverable.PointerPressed  += _hoverActions[i].OnPressed;
                button.Hoverable.PointerReleased += _hoverActions[i].OnReleased;
                button.Text.color = button.Style.NormalColor;
            }
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void OnHoverEntered(UIHoverableButton button)
        {
            button.Text.color = button.Style.HoverColor;
        }

        private void OnHoverExited(UIHoverableButton button)
        {
            button.Text.color = button.Style.NormalColor;
        }

        private void OnPointerPressed(UIHoverableButton button)
        {
            button.Text.color = button.Style.PressedColor;
        }

        private void OnPointerReleased(UIHoverableButton button)
        {
            button.Text.color = button.Style.NormalColor; // ← NormalColor en vez de HoverColor
        }

        // ─── API pública ──────────────────────────────────────────────
        public void ResetColors()
        {
            foreach (UIHoverableButton button in _buttons)
            {
                button.Text.color = button.Style.NormalColor;
            }
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Hoverable.HoverEntered    -= _hoverActions[i].OnEnter;
                _buttons[i].Hoverable.HoverExited     -= _hoverActions[i].OnExit;
                _buttons[i].Hoverable.PointerPressed  -= _hoverActions[i].OnPressed;
                _buttons[i].Hoverable.PointerReleased -= _hoverActions[i].OnReleased;
            }
        }
    }
}