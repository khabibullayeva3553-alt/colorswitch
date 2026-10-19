using UnityEngine;

/// <summary>Shar ko'tarilgan sari yangi halqa va rang o'zgartirgichlarni yaratib boradi.</summary>
public class ObstacleSpawner : MonoBehaviour
{
    public GameObject ringPrefab;
    public GameObject changerPrefab;
    public float nextY = 16.5f;
    public float spacing = 6.5f;
    public float aheadDistance = 14f;

    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        while (nextY < cam.position.y + aheadDistance)
        {
            GameObject ring = Instantiate(ringPrefab, new Vector3(0f, nextY, 0f), Quaternion.identity);
            ObstacleRing r = ring.GetComponent<ObstacleRing>();
            r.rotationSpeed = Random.Range(70f, 120f) * (Random.value < 0.5f ? -1f : 1f);

            Instantiate(changerPrefab, new Vector3(0f, nextY - spacing * 0.5f, 0f), Quaternion.identity);
            nextY += spacing;
        }
    }
}
