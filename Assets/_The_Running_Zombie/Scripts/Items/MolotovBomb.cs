using UnityEngine;
using System.Collections;

public class MolotovBomb : Item
{
    [SerializeField] private Animator _explosionAnimator;
    [SerializeField] private GameObject _body;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int initialDamage = 5;
    [SerializeField] private int burnDamage = 5;
    [SerializeField] private float burnDuration = 10f;

    // DŸwiêk wybuchu
    [SerializeField] private AudioClip explosionSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        // Dodanie AudioSource, jeœli brak
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        _body.SetActive(false);
        _explosionAnimator.Play("Fire");

        // Odtwarzanie dŸwiêku wybuchu
        PlayExplosionSound();

        ApplyInitialDamage(zombieHealth);
        StartCoroutine(ApplyBurning(zombieHealth));
    }

    protected override void OnGroundCollision()
    {
        base.OnGroundCollision();
        _body.SetActive(false);

        // Odtwarzanie dŸwiêku wybuchu
        PlayExplosionSound();

        // Odtwórz animacjê dwukrotnie
        StartCoroutine(PlayExplosionTwice());

        StartCoroutine(ApplyExplosionBurning());
        StartCoroutine(DestroyAfterExplosion());
    }

    private IEnumerator PlayExplosionTwice()
    {
        _explosionAnimator.Play("Fire");
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);
        _explosionAnimator.Play("Fire");
    }

    private void ApplyInitialDamage(ZombieStateAndHealth zombieHealth)
    {
        zombieHealth.TakeDamage(initialDamage);
    }

    private IEnumerator ApplyExplosionBurning()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
            {
                ApplyInitialDamage(zombieHealth);
                StartCoroutine(ApplyBurning(zombieHealth));
            }
        }
    }

    private IEnumerator ApplyBurning(ZombieStateAndHealth zombieHealth)
    {
        float elapsed = 0f;
        while (elapsed < burnDuration)
        {
            yield return new WaitForSeconds(5f);
            zombieHealth.TakeDamage(burnDamage);
            elapsed += 5f;
        }
    }

    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    // Funkcja odtwarzaj¹ca dŸwiêk wybuchu
    private void PlayExplosionSound()
    {
        if (explosionSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(explosionSound);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
