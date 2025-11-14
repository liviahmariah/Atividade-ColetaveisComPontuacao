using UnityEngine;

public class SnowFall : MonoBehaviour
{
    private float speed;

    public void Init(MonoBehaviour spawner, float fallSpeed)
    {
        speed = fallSpeed;
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
