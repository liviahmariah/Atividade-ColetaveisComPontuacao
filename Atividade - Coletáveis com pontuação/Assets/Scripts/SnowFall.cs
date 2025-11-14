using UnityEngine;

public class SnowFall : MonoBehaviour
{
    public float velocidade = 10f;
    public GameObject neveAcumuladaPrefab; // prefab da neve parada no chão

    private Terrain terreno;

    void Start()
    {
        terreno = Terrain.activeTerrain;
    }

    void Update()
    {
        transform.position += Vector3.down * velocidade * Time.deltaTime;

        // altura do terreno para ver onde está o chão
        float alturaChao = terreno.SampleHeight(transform.position);

        // quando tocar o chão
        if (transform.position.y <= alturaChao + 0.1f)
        {
            CriarNeveAcumulada();
            Destroy(gameObject);
        }
    }

    void CriarNeveAcumulada()
    {
        Vector3 pos = transform.position;
        pos.y = terreno.SampleHeight(pos);

        // cria um pontinho de neve no chão
        Instantiate(neveAcumuladaPrefab, pos, Quaternion.identity);
    }
}
