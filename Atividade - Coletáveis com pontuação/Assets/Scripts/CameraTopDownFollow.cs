using UnityEngine;

public class CameraTopDownOffset : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 10, -5);
    public float suavidade = 5f;

    void LateUpdate()
    {
        if (player != null)
        {
            // Aplica a rotação do player no offset
            Vector3 posicaoAlvo = player.position + player.rotation * offset;

            // Movimento suave até a posição
            transform.position = Vector3.Lerp(transform.position, posicaoAlvo, Time.deltaTime * suavidade);

            // Faz a câmera olhar para o player
            transform.LookAt(player.position);
        }
    }
}
