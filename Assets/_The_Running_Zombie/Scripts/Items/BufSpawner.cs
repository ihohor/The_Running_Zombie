using UnityEngine;

public class BufSpawner : MonoBehaviour
{
    public GameObject speedBufPrefab; // Poprawiono nazw� prefabrykat�w
    public float spawnInterval = 5f;
    public float minX = -10f;
    public float maxX = 10f;

    private void Start()
    {
        // Wywo�anie metody SpawnBuf co okre�lony czas
        InvokeRepeating("SpawnBuf", 0f, spawnInterval);
    }

    private void SpawnBuf()
    {
        // Losujemy pozycj� X w okre�lonym zakresie
        float spawnX = Random.Range(minX, maxX);

        // Pozycja spawnu tu� nad kamer� (y na podstawie rozmiaru kamery ortograficznej)
        Vector2 spawnPosition = new Vector2(spawnX, Camera.main.orthographicSize + 1f);

        // Spawnowanie bufa
        Instantiate(speedBufPrefab, spawnPosition, Quaternion.identity);
    }
}
