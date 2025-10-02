using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController3D : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 6f;

    [Header("Ground Check (opcional)")]
    public Transform groundCheck;       
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;      

    [Header("Debug")]
    public bool debugMode = false;

    private Rigidbody rb;
    private bool jumpRequest;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jumpRequest = true;
    }

    void FixedUpdate()
    {
        
        float moveX = Input.GetAxisRaw("Horizontal"); 
        float moveZ = Input.GetAxisRaw("Vertical");   

        Vector3 movimento = new Vector3(moveX, 0f, moveZ).normalized * velocidade * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movimento);

        
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
            if (debugMode) Debug.Log($"CheckSphere Grounded={isGrounded}");
        }
        else
        {
            
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
            if (debugMode) Debug.Log($"Raycast Grounded={isGrounded}");
        }

        
        if (jumpRequest && isGrounded)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            if (debugMode) Debug.Log("PULOU!");
        }

        jumpRequest = false;
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
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.1f);
        }
    }
}
