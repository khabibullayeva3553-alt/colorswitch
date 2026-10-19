using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aylanuvchi 4 rangli halqa (to'siq). Shar faqat o'z rangidagi bo'lakdan o'tishi mumkin.
/// Bo'lak i - Palette[i] rangida, halqa lokal burchagi i*90..(i+1)*90 oralig'ini egallaydi.
/// </summary>
public class ObstacleRing : MonoBehaviour
{
    public static readonly List<ObstacleRing> All = new List<ObstacleRing>();

    public float rotationSpeed = 90f;
    public float outerRadius = 1.6f;
    public float innerRadius = 1.3f;
    [HideInInspector] public bool scored;

    Transform cam;

    void OnEnable() { All.Add(this); }
    void OnDisable() { All.Remove(this); }

    void Start()
    {
        cam = Camera.main != null ? Camera.main.transform : null;
        for (int i = 0; i < 4; i++)
        {
            Transform seg = transform.Find("Seg" + i);
            if (seg != null) seg.GetComponent<SpriteRenderer>().color = GameColors.Palette[i];
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        if (cam != null && transform.position.y < cam.position.y - 14f) Destroy(gameObject);
    }

    /// <summary>Shar halqaga noto'g'ri rangda tegsa true qaytaradi.</summary>
    public bool Hits(Vector2 ballPos, float ballRadius, int ballColor)
    {
        Vector2 d = ballPos - (Vector2)transform.position;
        float dist = d.magnitude;
        float mid = (outerRadius + innerRadius) * 0.5f;
        float half = (outerRadius - innerRadius) * 0.5f;

        if (Mathf.Abs(dist - mid) > half + ballRadius * 0.6f) return false;

        float ang = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg - transform.eulerAngles.z;
        ang = Mathf.Repeat(ang, 360f);
        int quadrant = Mathf.Clamp(Mathf.FloorToInt(ang / 90f), 0, 3);
        return quadrant != ballColor;
    }
}
