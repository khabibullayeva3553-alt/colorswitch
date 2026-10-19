using UnityEngine;

/// <summary>Kamera shar ortidan faqat yuqoriga ergashadi (pastga qaytmaydi).</summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offsetY = 1.5f;
    public float smooth = 6f;

    float maxY;

    void Awake()
    {
        maxY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;
        float want = target.position.y + offsetY;
        if (want > maxY) maxY = want;

        Vector3 p = transform.position;
        p.y = Mathf.Lerp(p.y, maxY, 1f - Mathf.Exp(-smooth * Time.deltaTime));
        transform.position = p;
    }
}
