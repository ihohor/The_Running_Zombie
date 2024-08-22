using UnityEngine;
using System.Collections;

public class HomingRocket : MonoBehaviour
{
    [SerializeField] private Animator _explosionAnimator;
    [SerializeField] private GameObject _body;
    [SerializeField] private int damage = 50;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float speed = 10f; // Szybko�� lotu
    [SerializeField] private float rotationSpeed = 100f; // Szybko�� obracania si� w kierunku celu
    [SerializeField] private float detectionRadius = 10f; // Zasi�g wykrywania celu

    private Rigidbody2D rb;
    private Transform _target;
    private bool hasTarget = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Rakieta pocz�tkowo leci prosto
        rb.velocity = transform.up * speed;
    }

    private void Update()
    {
        if (!hasTarget)
        {
            SearchForTarget();
        }
        else if (_target != null)
        {
            Vector2 direction = (Vector2)_target.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotationSpeed;

            rb.velocity = transform.up * speed;
        }
    }

    private void SearchForTarget()
    {
        Collider2D[] potentialTargets = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in potentialTargets)
        {
            if (collider.CompareTag("Player"))
            {
                _target = collider.transform;
                hasTarget = true;
                break;
            }
            else if (collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
            {
                _target = zombieHealth.transform;
                hasTarget = true;
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();

        if (collision.collider.TryGetComponent<ZombieStateAndHealth>(out ZombieStateAndHealth zombieHealth))
        {
            zombieHealth.TakeDamage(damage);
        }
    }

    private void Explode()
    {
        _body.SetActive(false);
        _explosionAnimator.Play("ExplosionSB");

        StartCoroutine(ApplyExplosionDamage());
        StartCoroutine(DestroyAfterExplosion());
    }

    private IEnumerator ApplyExplosionDamage()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
            {
                zombieHealth.TakeDamage(damage);
            }
        }
    }

    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
