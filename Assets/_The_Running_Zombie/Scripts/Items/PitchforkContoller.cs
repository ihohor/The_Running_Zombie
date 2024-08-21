using UnityEngine;

public class PitchforkController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 50;

    private Rigidbody2D rb;
    private bool hasHit = false;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        ThrowTowardsTarget(targetPosition);
    }

    private void Update()
    {
        if (!hasHit)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void ThrowTowardsTarget(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Player"))
        {
            hasHit = true;

            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            transform.rotation = Quaternion.Euler(0, 0, 90f * Mathf.Sign(transform.localScale.x));

            if (collision.collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
            {
                zombieHealth.TakeDamage(damage);
            }

            Destroy(gameObject, 5f);
        }
    }
}
