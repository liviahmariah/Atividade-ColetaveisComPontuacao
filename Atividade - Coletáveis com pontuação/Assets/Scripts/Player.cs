using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController3D : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 6f;
    public float gravidade = -9.81f;
    public float velocidadeRotacao = 10f;

    [Header("Referência da Câmera")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocidadeJogador;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform; 
    }

    void Update()
    {
    
        isGrounded = controller.isGrounded;

        if (isGrounded && velocidadeJogador.y < 0)
            velocidadeJogador.y = -2f;

       
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 move = (forward * moveZ + right * moveX).normalized;


        controller.Move(move * velocidade * Time.deltaTime);

        
        if (move.magnitude >= 0.1f)
        {
            Quaternion rotacaoAlvo = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, velocidadeRotacao * Time.deltaTime);
        }

       
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocidadeJogador.y = Mathf.Sqrt(forcaPulo * -2f * gravidade);

     
        velocidadeJogador.y += gravidade * Time.deltaTime;


        controller.Move(velocidadeJogador * Time.deltaTime);
    }
}
