using UnityEngine;

/// <summary>Shar halok bo'lganda sochiladigan mayda bo'laklar.</summary>
public class Shard : MonoBehaviour
{
    public Vector2 velocity;
    public float life = 0.8f;

    float t;
    Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        t += Time.deltaTime;
        velocity += Vector2.down * 6f * Time.deltaTime;
        transform.position += (Vector3)(velocity * Time.deltaTime);
        float k = Mathf.Max(0f, 1f - t / life);
        transform.localScale = startScale * k;
        if (t >= life) Destroy(gameObject);
    }
}
