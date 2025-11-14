using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    public int pontos;  // você pode atualizar isso no jogo

    private void Start()
    {
        // Carrega automaticamente ao iniciar a cena
        SaveData data = SaveSystem.CarregarJogo();

        if (data != null)
        {
            Vector3 pos = new Vector3(data.playerX, data.playerY, data.playerZ);
            transform.position = pos;
            pontos = data.pontos;

            Debug.Log("Save carregado!");
        }
    }

    private void Update()
    {
        // CTRL + S = Salvar
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.SalvarJogo(transform.position, pontos);
            Debug.Log("Jogo salvo com CTRL + S");
        }
    }
}
