using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab monety
    public float spawnInterval = 2f; // Czas miêdzy spadkami monet
    public float minX = -10f; // Minimalna wspó³rzêdna X dla spawn
    public float maxX = 10f;  // Maksymalna wspó³rzêdna X dla spawn
    public float spawnHeight = 5f; // Wysokoœæ spawnu monet

    private void Start()
    {
        InvokeRepeating("SpawnCoin", 0f, spawnInterval);
    }

    private void SpawnCoin()
    {
        float spawnX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(spawnX, spawnHeight);
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
}
