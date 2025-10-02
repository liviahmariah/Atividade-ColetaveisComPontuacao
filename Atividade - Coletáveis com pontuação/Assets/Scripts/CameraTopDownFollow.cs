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
         
            Vector3 posicaoAlvo = player.position + offset;

           
            transform.position = Vector3.Lerp(transform.position, posicaoAlvo, Time.deltaTime * suavidade);

          
            transform.LookAt(player.position);
        }
    }
}
