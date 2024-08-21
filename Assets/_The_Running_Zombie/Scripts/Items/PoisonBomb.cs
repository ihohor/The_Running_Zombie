using UnityEngine;
using System.Collections;

public class PoisonBomb : Item
{
    [SerializeField] private Animator _explosionAnimator;
    [SerializeField] private GameObject _body;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int poisonDamage = 3; // Obra¿enia od trucizny
    [SerializeField] private float poisonDuration = 5f; // Czas trwania trucizny

    private void OnEnable()
    {
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        _body.SetActive(false);
        _explosionAnimator.Play("PoisonBomb");
        StartCoroutine(ApplyPoison(zombieHealth));
    }

    protected override void OnGroundCollision()
    {
        base.OnGroundCollision();
        _body.SetActive(false);
        _explosionAnimator.Play("PoisonBomb");

        StartCoroutine(ApplyExplosionPoison());
        StartCoroutine(DestroyAfterExplosion());
    }

    private IEnumerator ApplyExplosionPoison()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
            {
                StartCoroutine(ApplyPoison(zombieHealth));
            }
        }
    }

    private IEnumerator ApplyPoison(ZombieStateAndHealth zombieHealth)
    {
        float elapsed = 0f;
        while (elapsed < poisonDuration)
        {
            zombieHealth.TakeDamage(poisonDamage); // Zadaj obra¿enia od trucizny co sekundê
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
