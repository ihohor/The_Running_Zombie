using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab eksplozji
    private Rigidbody2D rb;
    public bool explode = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Debug

        if (collision.gameObject.CompareTag("Ground"))
        {
            HandleCollision();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Ustawienie stanu Dead na true, aby uruchomiæ animacjê œmierci
            ZombieController zombie = collision.gameObject.GetComponent<ZombieController>();
            if (zombie != null)
            {
                zombie.Dead = true;
            }

            HandleCollision();
        }

        Debug.Log("Bomba uderzy³a w: " + collision.collider.name);
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Player"))
        {
            Explode();
        }

    }

    private void HandleCollision()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Zatrzymuje fizykê

        Invoke("Explode", 2f); // Wywo³aj eksplozjê po 2 sekundach
    }

    void Explode()
    {
        Debug.Log("Explode called"); // Sprawdzi, czy funkcja jest wywo³ywana
        explode = true;
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("Explosion prefab instantiated");
        }
        Destroy(gameObject); // Zniszcz bombê po eksplozji
    }

}

