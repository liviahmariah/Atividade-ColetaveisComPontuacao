using UnityEngine;

public class SnowSpawner: MonoBehaviour
{
    public GameObject flocoPrefab;          // O floco que cai
    public GameObject neveAcumuladaPrefab;  // A neve que fica no chão

    public float intensidade = 200f;        // Quantos flocos caem por segundo
    public float alturaExtra = 20f;         // Altura acima do chão onde nasce a neve
    public float velocidadeQueda = 10f;     // Velocidade da neve caindo

    private Terrain terreno;
    private TerrainData dados;
    private float acumulador;

    void Start()
    {
        terreno = Terrain.activeTerrain;
        dados = terreno.terrainData;
    }

    void Update()
    {
        // Quantos flocos devem cair neste frame
        float flocosPorFrame = intensidade * Time.deltaTime;

        acumulador += flocosPorFrame;

        while (acumulador >= 1f)
        {
            SpawnFloco();
            acumulador -= 1f;
        }
    }

    void SpawnFloco()
    {
        float largura = dados.size.x;
        float comprimento = dados.size.z;

        // posição aleatória no terreno
        float x = Random.Range(0, largura);
        float z = Random.Range(0, comprimento);

        // altura do terreno
        float yTerreno = terreno.SampleHeight(new Vector3(x, 0, z));

        // posição final onde o floco nasce
        Vector3 posFinal = new Vector3(
            x,
            yTerreno + alturaExtra,
            z
        );

        GameObject floco = Instantiate(flocoPrefab, posFinal, Quaternion.identity);

        // adiciona comportamento de queda
        SnowFall queda = floco.AddComponent<SnowFall>();
        queda.velocidade = velocidadeQueda;
        queda.neveAcumuladaPrefab = neveAcumuladaPrefab;
    }
}
