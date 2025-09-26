using UnityEngine;
public class movimento3d : MonoBehaviour
{
    [Header("Configuração de Movimento")]
    public float moveSpeed = 5f;         // Velocidade de movimento
    public float jumpForce = 5f;         // Força do pulo

    [Header("Configuração do Mouse")]
    public float mouseSensitivity = 200f; // Sensibilidade do mouse
    public Transform playerCamera;       // Câmera do Player

    private Rigidbody rb;                // Referência ao Rigidbody
    private bool isGrounded = true;      // Controle do pulo
    private float xRotation = 0f;        // Controle da rotação vertical

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Travar rotação física do Player para não tombar
        rb.freezeRotation = true;

        // Bloquear e esconder o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimento do Mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotação vertical (câmera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita olhar pra cima/baixo
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotação horizontal (player)
        transform.Rotate(Vector3.up * mouseX);

        // Movimento Horizontal/Vertical
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

        // Pulo 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}