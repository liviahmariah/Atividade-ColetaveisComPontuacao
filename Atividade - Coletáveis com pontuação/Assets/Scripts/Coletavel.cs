using UnityEngine;

public class Coletavel : MonoBehaviour
{
    public int valor = 1; 

    void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag("Player"))
        {
            Pontuacao.instance.AdicionarPontos(valor); 
            Destroy(gameObject); 
            Pontuacao.instance.AdicionarPontos(valor);
        }
    }
}
