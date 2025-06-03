using UnityEngine;

// Requiere que el GameObject tenga un componente Rigidbody (física)
[RequireComponent(typeof(Rigidbody))]
public class BallMovement : MonoBehaviour
{
    // [Header] crea encabezados organizados en el Inspector de Unity
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 40f;     // Fuerza aplicada al movimiento
    
    [Header("Physics Settings")]
    [SerializeField] private float groundDrag = 5f;     // Resistencia en el suelo
    [SerializeField] private float airDrag = 1f;        // Resistencia en el aire
    
    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckDistance = 0.6f;  // Distancia para detectar el suelo
    
    // Referencias y estado interno
    private Rigidbody              _rb;                 // Referencia al componente Rigidbody
    private float                  _horizontalInput;    // Input horizontal (teclas A/D o flechas)
    private float                  _verticalInput;      // Input vertical (teclas W/S o flechas)
    private bool                   _isGrounded;         // Variable para validar si la bola está tocando el suelo?
    
    
    // Awake se ejecuta al cargar el script, ideal para inicializaciones de referencias
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();    // Obtener referencia al Rigidbody adjunto
    }

    private void Update()
    {
        GetInput();         // Leer entradas del teclado
        CheckGrounded();    // Verificar si está tocando el suelo
        ControlDrag();      // Ajustar resistencia física
    }

    // Se ejecuta en intervalos fijos. Ideal para aplicar fuerzas y manipular física.
    private void FixedUpdate()
    {
        MoveBall();         // Aplicar movimiento físico
    }
    
    /// <summary>
    /// Lee la entrada del jugador en los ejes horizontales y verticales.
    /// </summary>
    private void GetInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");  // Eje horizontal (A/D)
        _verticalInput = Input.GetAxisRaw("Vertical");      // Eje vertical (W/S)
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
    
    /// <summary>
    /// Ajusta el valor de drag del Rigidbody según si la bola está en el aire o en el suelo.
    /// (drag) para simular la friccion
    /// </summary>
    private void ControlDrag()
    {
        _rb.drag = _isGrounded ? groundDrag : airDrag;
    }
    
    /// <summary>
    /// Verifica si la bola está tocando el suelo usando un Raycast.
    /// </summary>
    private void CheckGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
    
    /// <summary>
    /// Dibuja una línea en la escena para visualizar la comprobación del suelo.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
