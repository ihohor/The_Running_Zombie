using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab; // Prefab bomby
    public float spawnInterval = 2f; // Czas miêdzy kolejnymi spadkami
    public float minX = -10f; // Minimalna wspó³rzêdna X dla spawn
    public float maxX = 10f;  // Maksymalna wspó³rzêdna X dla spawn

    private void Start()
    {
        // Uruchom corutynê do spawnowania bomb
        InvokeRepeating("SpawnBomb", 0f, spawnInterval);
    }

    private void SpawnBomb()
    {
        // Wylosuj pozycjê X w zakresie minX i maxX
        float spawnX = Random.Range(minX, maxX);

        // Ustawienia pozycji spawn w górnej czêœci ekranu
        Vector2 spawnPosition = new Vector2(spawnX, Camera.main.orthographicSize + 1f);

        // Utwórz bombê
        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }
}
