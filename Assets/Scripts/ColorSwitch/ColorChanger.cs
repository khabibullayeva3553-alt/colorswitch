using UnityEngine;

/// <summary>Shar yig'ib oladigan rang o'zgartirgich. Tegsa, shar boshqa rangga o'tadi.</summary>
public class ColorChanger : MonoBehaviour
{
    SpriteRenderer sr;
    Vector3 baseScale;
    Transform cam;
    float t;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;
    }

    void Start()
    {
        cam = Camera.main != null ? Camera.main.transform : null;
        t = Random.value;
    }

    void Update()
    {
        t += Time.deltaTime * 0.8f;
        sr.color = Color.HSVToRGB(Mathf.Repeat(t, 1f), 0.65f, 1f);
        transform.localScale = baseScale * (1f + 0.12f * Mathf.Sin(Time.time * 5f));
        if (cam != null && transform.position.y < cam.position.y - 14f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BallController ball = other.GetComponent<BallController>();
        if (ball == null) return;
        ball.ChangeColor();
        Destroy(gameObject);
    }
}
