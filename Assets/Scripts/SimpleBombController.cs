using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab wybuchu
    public float timeToExplode = 2.0f; // Czas do wybuchu w sekundach

    private void Start()
    {
        Debug.Log("Bomb has spawned. Explosion in " + timeToExplode + " seconds.");
        Invoke("Explode", timeToExplode);
    }

    private void Explode()
    {
        Debug.Log("Bomb exploded!");

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("Explosion prefab instantiated.");
        }

        Destroy(gameObject);
    }
}
