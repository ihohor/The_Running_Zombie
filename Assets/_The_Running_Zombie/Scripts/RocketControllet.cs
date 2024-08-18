using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float speed = 2f; // Prêdkoœæ rakiety
    public GameObject explosionPrefab; // Prefab wybuchu

    private Transform target; // Cel (np. postaæ gracza)
    private Rigidbody2D rb;

    private void Start()
    {
        // ZnajdŸ obiekt gracza po tagu
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        // Ustaw rakietê w kierunku gracza
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Ustaw pocz¹tkowy obrót rakiety w kierunku lotu
        RotateTowards(direction);
    }

    private void Update()
    {
        // Aktualizuj kierunek obrotu rakiety w locie
        RotateTowards(rb.velocity.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // SprawdŸ, czy rakieta trafia w gracza lub w inne obiekty
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
        {
            Explode();
        }
    }

    private void RotateTowards(Vector2 direction)
    {
        // Oblicz k¹t do obrotu
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ustaw rotacjê rakiety
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Explode()
    {
        // Instancjonuj wybuch
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject); // Zniszcz rakietê
    }
}
