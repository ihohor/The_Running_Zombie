using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float speed = 2f; // Pr�dko�� rakiety
    public GameObject explosionPrefab; // Prefab wybuchu

    private Transform target; // Cel (np. posta� gracza)
    private Rigidbody2D rb;

    private void Start()
    {
        // Znajd� obiekt gracza po tagu
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        // Ustaw rakiet� w kierunku gracza
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Ustaw pocz�tkowy obr�t rakiety w kierunku lotu
        RotateTowards(direction);
    }

    private void Update()
    {
        // Aktualizuj kierunek obrotu rakiety w locie
        RotateTowards(rb.velocity.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawd�, czy rakieta trafia w gracza lub w inne obiekty
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
        {
            Explode();
        }
    }

    private void RotateTowards(Vector2 direction)
    {
        // Oblicz k�t do obrotu
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ustaw rotacj� rakiety
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Explode()
    {
        // Instancjonuj wybuch
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject); // Zniszcz rakiet�
    }
}
