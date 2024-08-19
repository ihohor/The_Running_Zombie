using UnityEngine;

public class SimpleExplosion : MonoBehaviour
{
    public float explosionRadius = 5f; // Promieñ eksplozji
    public int explosionDamage = 50; // Obra¿enia eksplozji

    void Start()
    {
        // Wywo³anie eksplozji po opóŸnieniu (jeœli jest wymagane)
        Invoke("Explode", 0.1f);
    }

    void Explode()
    {
        // Wyœwietlenie efektu eksplozji (jeœli jest u¿ywany)
        // Instantiate(explosionEffect, transform.position, transform.rotation);

        // Wykrywanie obiektów w promieniu eksplozji
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player")) // Sprawdzenie, czy obiekt jest zombie
            {
                // Znalezienie komponentu ZombieStateAndHealth i zadanie obra¿eñ
                ZombieStateAndHealth zombieHealth = collider.GetComponent<ZombieStateAndHealth>();
                if (zombieHealth != null)
                {
                    zombieHealth.TakeDamage(explosionDamage);
                }
            }
        }

        // Opcjonalne: zniszczenie bomby po eksplozji
        Destroy(gameObject);
    }

    // Wizualizacja promienia eksplozji (dla celów debugowania)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
