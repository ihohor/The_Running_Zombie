using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public GameObject rocketPrefab; // Prefab rakiety
    public float spawnInterval = 5f; // Czas mi�dzy spawnowaniem
    public Transform leftSpawnPoint; // Lewy punkt spawnu
    public Transform rightSpawnPoint; // Prawy punkt spawnu

    private void Start()
    {
        // Uruchom powtarzaj�ce si� wywo�ywanie spawnowania rakiet
        InvokeRepeating("SpawnRocket", 2f, spawnInterval);
    }

    private void SpawnRocket()
    {
        // Losowo wybierz stron� (lewa lub prawa)
        Transform spawnPoint = Random.value < 0.5f ? leftSpawnPoint : rightSpawnPoint;

        // Instancjonuj rakiet�
        Instantiate(rocketPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
