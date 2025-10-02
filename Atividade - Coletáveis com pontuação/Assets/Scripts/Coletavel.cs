using UnityEngine;

public class Coletavel : MonoBehaviour
{
    public int valor = 1; // quantos pontos vale

    void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag("Player"))
        {
            Pontuacao.instance.AdicionarPontos(valor); // adiciona pontos
            Destroy(gameObject); // remove o coletável
            Pontuacao.instance.AdicionarPontos(valor);
        }
    }
}
