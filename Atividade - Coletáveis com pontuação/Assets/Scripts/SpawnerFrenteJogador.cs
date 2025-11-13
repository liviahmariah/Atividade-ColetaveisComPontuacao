using UnityEngine;

public class SpawnerFrenteJogador : MonoBehaviour
{
    public GameObject prefab;          // Prefab da moeda
    public Transform jogador;          // Referência ao jogador
    public int quantidade = 10;        // Quantas moedas no total (fila)
    public float distanciaEntre = 1.5f; // Distância entre moedas
    public float intervalo = 1f;       // Tempo entre spawns
    public float raioVerificacao = 0.3f;
    public LayerMask paredes;          // Layer das paredes (opcional)

    private int indiceAtual = 0;
    private float tempo;

    void Update()
    {
        if (jogador == null) return; // Evita erro se o jogador não estiver atribuído

        tempo += Time.deltaTime;
        if (tempo >= intervalo)
        {
            SpawnarMoeda();
            tempo = 0f;
        }
    }

    void SpawnarMoeda()
    {
        // Calcula posição da nova moeda: sempre à frente do jogador
        Vector3 pos = jogador.position + jogador.forward * (distanciaEntre * (indiceAtual + 1));

        // Verifica se há parede ou obstáculo no ponto
        bool temParede = Physics.CheckSphere(pos, raioVerificacao, paredes);
        if (!temParede)
        {
            Instantiate(prefab, pos, Quaternion.identity);
        }

        // Próxima moeda vai um pouco mais à frente
        indiceAtual++;

        // Reinicia a contagem se já atingiu o limite da fila
        if (indiceAtual >= quantidade)
        {
            indiceAtual = 0;
        }
    }
}
