using UnityEngine;

public class SimpleBomb : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab eksplozji
    public int damage = 50; // Obra¿enia zadawane przez bombê

    private bool hasExploded = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawdzamy, czy bomba zderzy³a siê z zombie i czy ju¿ nie wybuch³a
        if (collision.collider.CompareTag("Player") && !hasExploded)
        {
            Explode(collision.contacts[0].point); // U¿ywamy punktu kontaktu do wywo³ania eksplozji
            ZombieStateAndHealth zombieHealth = collision.collider.GetComponent<ZombieStateAndHealth>();

            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(damage);
            }
        }
    }

    void Explode(Vector2 explosionPosition)
    {
        // Ustawiamy eksplozjê w miejscu kolizji
        Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);

        // Flaga, ¿eby nie wywo³ywaæ wielokrotnie eksplozji
        hasExploded = true;

        // Usuwamy bombê po eksplozji
        Destroy(gameObject);
    }
}
