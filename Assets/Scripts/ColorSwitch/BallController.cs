using UnityEngine;

/// <summary>Sakrab ko'tariladigan shar. Rigidbody2D fizikasi bilan ishlaydi.</summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float jumpVelocity = 8.5f;
    public float gravity = 3f;
    public float radius = 0.2f;
    public int colorIndex;

    Rigidbody2D rb;
    SpriteRenderer sr;
    bool dead;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;
    }

    public void Begin()
    {
        rb.gravityScale = gravity;
    }

    public void Jump()
    {
        if (dead) return;
        rb.linearVelocity = new Vector2(0f, jumpVelocity);
    }

    public void SetColor(int index)
    {
        colorIndex = index % GameColors.Palette.Length;
        sr.color = GameColors.Palette[colorIndex];
    }

    public void ChangeColor()
    {
        int n;
        do { n = Random.Range(0, GameColors.Palette.Length); } while (n == colorIndex);
        SetColor(n);
    }

    public void Die()
    {
        if (dead) return;
        dead = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        sr.enabled = false;

        for (int i = 0; i < 16; i++)
        {
            GameObject go = new GameObject("Shard");
            go.transform.position = transform.position;
            go.transform.localScale = Vector3.one * Random.Range(0.06f, 0.14f);
            SpriteRenderer s = go.AddComponent<SpriteRenderer>();
            s.sprite = sr.sprite;
            s.color = sr.color;
            s.sortingOrder = 20;
            Shard sh = go.AddComponent<Shard>();
            sh.velocity = Random.insideUnitCircle.normalized * Random.Range(2f, 6f);
        }
    }
}
