using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public GameObject rocketPrefab; // Prefab rakiety
    public float spawnInterval = 5f; // Czas miêdzy spawnowaniem
    public Transform leftSpawnPoint; // Lewy punkt spawnu
    public Transform rightSpawnPoint; // Prawy punkt spawnu

    private void Start()
    {
        // Uruchom powtarzaj¹ce siê wywo³ywanie spawnowania rakiet
        InvokeRepeating("SpawnRocket", 2f, spawnInterval);
    }

    private void SpawnRocket()
    {
        // Losowo wybierz stronê (lewa lub prawa)
        Transform spawnPoint = Random.value < 0.5f ? leftSpawnPoint : rightSpawnPoint;

        // Instancjonuj rakietê
        Instantiate(rocketPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
