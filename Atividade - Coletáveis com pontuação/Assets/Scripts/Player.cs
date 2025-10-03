using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController3D : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 6f;
    public float velocidadeRotacao = 10f;

    [Header("Ground Check (opcional)")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    [Header("Debug")]
    public bool debugMode = false;

    private CharacterController controller;
    private Vector3 velocidadeVertical; // Para gravidade e pulo
    private bool jumpRequest;
    private bool isGrounded;
    private float gravidade = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jumpRequest = true;

        // Ground check
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
            if (debugMode) Debug.Log($"CheckSphere Grounded={isGrounded}");
        }
        else
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f + controller.height / 2f, groundMask);
            if (debugMode) Debug.Log($"Raycast Grounded={isGrounded}");
        }

        if (isGrounded && velocidadeVertical.y < 0)
        {
            velocidadeVertical.y = -2f; // Mantém o personagem "colado" ao chão
        }

        if (jumpRequest && isGrounded)
        {
            velocidadeVertical.y = Mathf.Sqrt(forcaPulo * -2f * gravidade);
            if (debugMode) Debug.Log("PULOU!");
        }

        jumpRequest = false;
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 direcao = new Vector3(moveX, 0f, moveZ).normalized;

        // Movimento horizontal
        Vector3 movimento = direcao * velocidade;

        // Aplica gravidade
        velocidadeVertical.y += gravidade * Time.fixedDeltaTime;

        // Move o personagem
        controller.Move((movimento + velocidadeVertical) * Time.fixedDeltaTime);

        // Rota��o suave
        if (direcao.magnitude >= 0.1f)
        {
            Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, velocidadeRotacao * Time.fixedDeltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (0.1f + (controller != null ? controller.height / 2f : 1f)));
        }
    }
}
