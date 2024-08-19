using UnityEngine;
using System.Collections;

public class FrozenBomb : Item
{
    [SerializeField] private Animator _explosionAnimator;
    [SerializeField] private GameObject _body;
    [SerializeField] private float slowDuration = 5f;
    [SerializeField] private float slowFactor = 0.5f;
    [SerializeField] private float explosionRadius = 5f;

    private void OnEnable()
    {
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        _body.SetActive(false);
        _explosionAnimator.Play("FrozenExplosion");
        ZombieMovement zombieMovement = zombieHealth.GetComponent<ZombieMovement>();
        if (zombieMovement != null)
        {
            StartCoroutine(ApplySlowEffect(zombieMovement));
        }
    }

    protected override void OnGroundCollision()
    {
        base.OnGroundCollision();
        _body.SetActive(false);
        _explosionAnimator.Play("FrozenExplosion");

        StartCoroutine(ApplyExplosionSlow());
        StartCoroutine(DestroyAfterExplosion());
    }

    private IEnumerator ApplyExplosionSlow()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
            {
                ZombieMovement zombieMovement = zombieHealth.GetComponent<ZombieMovement>();
                if (zombieMovement != null)
                {
                    StartCoroutine(ApplySlowEffect(zombieMovement));
                }
            }
        }
    }

    private IEnumerator ApplySlowEffect(ZombieMovement zombieMovement)
    {
        float originalSpeed = zombieMovement.speed;
        zombieMovement.speed *= slowFactor;

        yield return new WaitForSeconds(slowDuration);

        zombieMovement.speed = originalSpeed;
    }

    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
