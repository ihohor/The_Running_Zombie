using UnityEngine;

public class MedKitSpawner : MonoBehaviour
{
    public GameObject medKitPrefab;
    public float spawnInterval = 5f;
    public float minX = -10f;
    public float maxX = 10f;

    private void Start()
    {
        InvokeRepeating("SpawnMedKit", 0f, spawnInterval);
    }

    private void SpawnMedKit()
    {
        float spawnX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(spawnX, Camera.main.orthographicSize + 1f);
        Instantiate(medKitPrefab, spawnPosition, Quaternion.identity);
    }
}
