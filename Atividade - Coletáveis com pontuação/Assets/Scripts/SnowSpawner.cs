using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public GameObject snowPrefab;
    public float terrainSize = 500f;
    public float spawnHeight = 100f;
    public float fallSpeed = 2f;
    public int maxSnowflakes = 300;
    public float spawnInterval = 0.1f;

    private float timer = 0f;
    private int activeSnowflakes = 0;

    void Update()
    {
        timer += Time.deltaTime;

        // A cada intervalo, cria um novo floco
        if (timer >= spawnInterval && activeSnowflakes < maxSnowflakes)
        {
            SpawnSnowflake();
            timer = 0f;
        }
    }

    void SpawnSnowflake()
    {
        // Posição aleatória dentro do terreno
        Vector3 pos = new Vector3(
            Random.Range(0, terrainSize),
            spawnHeight,
            Random.Range(0, terrainSize)
        );

        GameObject flake = Instantiate(snowPrefab, pos, Quaternion.identity);
        activeSnowflakes++;

        // Faz o floco cair
        Rigidbody rb = flake.AddComponent<Rigidbody>();
        rb.useGravity = false;

        // Controla a queda manualmente
        flake.AddComponent<SnowFall>().Init(this, fallSpeed);
    }

    // Chamado quando o floco se destrói
    public void RemoveFlake()
    {
        activeSnowflakes--;
    }
}
