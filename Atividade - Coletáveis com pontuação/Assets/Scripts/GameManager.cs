using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform player;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Carregar();
    }

    public void Salvar()
    {
        SaveSystem.SalvarJogo(player.position, Pontuacao.instance.pontos);
        Debug.Log("Jogo salvo.");
    }

    public void Carregar()
    {
        SaveData data = SaveSystem.CarregarJogo();

        if (data != null)
        {
            Vector3 pos = new Vector3(data.playerX, data.playerY, data.playerZ);
            player.position = pos;

            Pontuacao.instance.pontos = data.pontos;
            Pontuacao.instance.AtualizarTexto();

            Debug.Log("Jogo carregado.");
        }
    }
}
