using UnityEngine;

public class PitchforkController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 50;
    [SerializeField] private float lifetimeAfterImpact = 10f;

    private Rigidbody2D rb;
    private bool hasHit = false;
    private Vector2 flightDirection;

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
        flightDirection = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasHit)
        {
            hasHit = true;

            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.freezeRotation = true;

            StickInTarget(collision);

            if (collision.collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
            {
                Vector2 hitNormal = collision.contacts[0].normal;
                if (Vector2.Dot(hitNormal, -flightDirection) > 0.5f)
                {
                    zombieHealth.TakeDamage(damage);
                }
            }

            Destroy(gameObject, lifetimeAfterImpact);
        }
    }

    private void StickInTarget(Collision2D collision)
    {
        Vector2 hitPoint = collision.contacts[0].point;
        transform.position = hitPoint;

        float angle = Mathf.Atan2(flightDirection.y, flightDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
