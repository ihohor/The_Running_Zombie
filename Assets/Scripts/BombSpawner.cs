using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab; // Prefab bomby
    public float spawnInterval = 2f; // Czas mi�dzy kolejnymi spadkami
    public float minX = -10f; // Minimalna wsp�rz�dna X dla spawn
    public float maxX = 10f;  // Maksymalna wsp�rz�dna X dla spawn

    private void Start()
    {
        // Uruchom corutyn� do spawnowania bomb
        InvokeRepeating("SpawnBomb", 0f, spawnInterval);
    }

    private void SpawnBomb()
    {
        // Wylosuj pozycj� X w zakresie minX i maxX
        float spawnX = Random.Range(minX, maxX);

        // Ustawienia pozycji spawn w g�rnej cz�ci ekranu
        Vector2 spawnPosition = new Vector2(spawnX, Camera.main.orthographicSize + 1f);

        // Utw�rz bomb�
        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }
}
