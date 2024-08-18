using UnityEngine;

public class SimpleBomb : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab eksplozji
    public int damage = 50; // Obra�enia zadawane przez bomb�

    private bool hasExploded = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawdzamy, czy bomba zderzy�a si� z zombie i czy ju� nie wybuch�a
        if (collision.collider.CompareTag("Player") && !hasExploded)
        {
            Explode(collision.contacts[0].point); // U�ywamy punktu kontaktu do wywo�ania eksplozji
            ZombieStateAndHealth zombieHealth = collision.collider.GetComponent<ZombieStateAndHealth>();

            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(damage);
            }
        }
    }

    void Explode(Vector2 explosionPosition)
    {
        // Ustawiamy eksplozj� w miejscu kolizji
        Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);

        // Flaga, �eby nie wywo�ywa� wielokrotnie eksplozji
        hasExploded = true;

        // Usuwamy bomb� po eksplozji
        Destroy(gameObject);
    }
}
