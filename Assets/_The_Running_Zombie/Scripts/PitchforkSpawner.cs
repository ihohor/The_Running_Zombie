using UnityEngine;

public class PitchforkSpawner : MonoBehaviour
{
    public GameObject pitchforkPrefab;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public float spawnInterval = 2.0f;

    private float timeSinceLastSpawn;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnPitchfork();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnPitchfork()
    {
        // Losowanie, czy wide³ wylatuje z lewej czy prawej strony
        Transform spawnPoint = (Random.Range(0, 2) == 0) ? leftSpawnPoint : rightSpawnPoint;

        // Tworzenie wide³
        Instantiate(pitchforkPrefab, spawnPoint.position, Quaternion.identity);
    }
}
