using UnityEngine;

public class PitchforkController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    private Rigidbody2D rb;
    private bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        ThrowTowardsPlayer(targetPosition);
    }

    void Update()
    {
        if (!hasHit)
        {
            // Obracanie wide³ w kierunku lotu
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void ThrowTowardsPlayer(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Player"))
        {
            hasHit = true;

            // Zatrzymanie wide³ po wbiciu
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            // Dodanie wbijania wide³ pionowo
            transform.rotation = Quaternion.Euler(0, 0, 90f * Mathf.Sign(transform.localScale.x));

            // Zniszczenie wide³ po 5 sekundach
            Destroy(gameObject, 5f);
        }
    }
}
