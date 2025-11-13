using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Transform player; 
    public int pontos;       

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Salvar();
        }

    
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Carregar();
        }
    }

    public void Salvar()
    {
        SaveSystem.SalvarJogo(player.position, pontos);
        Debug.Log("Jogo salvo!");
    }

    public void Carregar()
    {
        SaveData data = SaveSystem.CarregarJogo();
        if (data != null)
        {
            Vector3 posicao = new Vector3(data.playerX, data.playerY, data.playerZ);
            player.position = posicao;
            pontos = data.pontos;
            Debug.Log("Jogo carregado!");
        }
    }
}
 