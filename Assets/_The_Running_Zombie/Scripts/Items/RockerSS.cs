using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public GameObject rocketPrefab;
    public float spawnInterval = 5f;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    private void Start()
    {
        
        InvokeRepeating("SpawnRocket", 2f, spawnInterval);
    }

    private void SpawnRocket()
    {
        Transform spawnPoint = Random.value < 0.5f ? leftSpawnPoint : rightSpawnPoint;

        Instantiate(rocketPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
