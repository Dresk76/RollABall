using RollABall.Programming.Core.Managers;
using RollABall.Programming.GamePlay.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RollABall.Programming.GamePlay.Player
{
    // Requiere que el GameObject tenga un componente Rigidbody (física)
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour, IKeyRecovered
    {    
        // [Header] crea encabezados organizados en el Inspector de Unity
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 50f; // Fuerza aplicada al movimiento

        // Referencias y estado interno
        private Rigidbody _rb;               // Referencia al componente Rigidbody
        private float     _horizontalInput;  // Input horizontal (teclas A/D o flechas)
        private float     _verticalInput;    // Input vertical (teclas W/S o flechas)



        // Awake se ejecuta al cargar el script, ideal para inicializaciones de referencias
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();    // Obtener referencia al Rigidbody adjunto
        }

        // Se ejecuta en intervalos fijos. Ideal para aplicar fuerzas y manipular física.
        private void FixedUpdate()
        {
            MoveBall();         // Aplicar movimiento físico
        }

        /// <summary>
        /// Lee la entrada del jugador en los ejes horizontales y verticales.
        /// </summary>
        private void OnMove(InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            _horizontalInput = movementVector.x;
            _verticalInput = movementVector.y;
        }

        /// <summary>
        /// Aplica una fuerza a la bola según la dirección del input.
        /// </summary>
        private void MoveBall()
        {
            // Crear vector de dirección con los inputs (Y=0 para movimiento plano)
            // .normalized asegura que la velocidad sea igual a 1 en diagonales
            Vector3 moveDirection = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;
            
            // Solo aplicar fuerza si el jugador está presionando una dirección
            if (moveDirection != Vector3.zero)
            {
                _rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
            }
        }

        public void OnKeyRecovered(int keyValue)
        {
            //GameManager.Instance.AddKeys(keyValue);
        }
    }
}
