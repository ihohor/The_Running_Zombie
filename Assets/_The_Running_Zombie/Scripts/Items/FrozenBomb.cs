using UnityEngine;
using System.Collections;

public class FrozenBomb : Item
{
    [SerializeField] private Animator _explosionAnimator;
    [SerializeField] private GameObject _body;
    [SerializeField] private float slowDuration = 5f;
    [SerializeField] private float slowFactor = 0.5f;
    [SerializeField] private float explosionRadius = 5f;

    // Pole na d�wi�k wybuchu
    [SerializeField] private AudioClip explosionSound;

    private AudioSource _audioSource;
    private Collider2D _collider;

    private void Awake()
    {
        // Pobieramy AudioSource z obiektu lub dodajemy, je�li nie istnieje
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        _collider = GetComponent<Collider2D>();
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        _body.SetActive(false);
        _explosionAnimator.Play("FrozenExplosion");

        // Odtwarzanie d�wi�ku wybuchu
        PlayExplosionSound();

        DisableCollisionWithTag("Player");

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

        // Odtwarzanie d�wi�ku wybuchu
        PlayExplosionSound();

        DisableCollisionWithTag("Player");

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
        if (zombieMovement.IsSlowed)
        {
            zombieMovement.ExtendSlowDuration(slowDuration);
        }
        else
        {
            zombieMovement.IsSlowed = true;
            float originalSpeed = zombieMovement.speed;
            zombieMovement.speed *= slowFactor;

            yield return new WaitForSeconds(slowDuration);

            zombieMovement.speed = originalSpeed;
            zombieMovement.IsSlowed = false;
        }
    }

    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void DisableCollisionWithTag(string tag)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(tag))
            {
                Physics2D.IgnoreCollision(collider, _collider);
            }
        }
    }

    // Funkcja do odtwarzania d�wi�ku wybuchu
    private void PlayExplosionSound()
    {
        if (explosionSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(explosionSound);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
