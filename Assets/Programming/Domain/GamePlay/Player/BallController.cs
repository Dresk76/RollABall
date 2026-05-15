using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Domain.Enums;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RollABall.Domain.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour, ISceneInitializable, IDisposable
    {
        // ─── Campos ───────────────────────────────────────────────────
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 50f;

        private Rigidbody _rb;
        private IEventBus _eventBus;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _canMove = true;

        // ─── Ciclo de vida Unity ──────────────────────────────────────
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // ─── Inicialización ───────────────────────────────────────────
        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventBus.Subscribe<GameStateChangedEvent>(HandleGameStateChanged);
        }

        // ─── Input ────────────────────────────────────────────────────
        private void OnMove(InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            _horizontalInput = movementVector.x;
            _verticalInput = movementVector.y;
        }

        // ─── Física ───────────────────────────────────────────────────
        private void FixedUpdate()
        {
            if (!_canMove) return;
            MoveBall();
        }

        private void MoveBall()
        {
            Vector3 moveDirection = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;

            if (moveDirection != Vector3.zero)
            {
                _rb.AddForce(moveDirection * _moveSpeed, ForceMode.Force);
            }
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleGameStateChanged(GameStateChangedEvent e)
        {
            _canMove = e.NewState == GameState.Playing;
            _rb.isKinematic = !_canMove;
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
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