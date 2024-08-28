using UnityEngine;
using System.Collections;

public class PoisonBomb : Item
{
    [SerializeField] private Animator _explosionAnimator;
    [SerializeField] private GameObject _body;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int poisonDamage = 3; // Obra¿enia od trucizny
    [SerializeField] private float poisonDuration = 5f; // Czas trwania trucizny

    // Dodajemy dŸwiêk wybuchu
    [SerializeField] private AudioClip explosionSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        // Sprawdzenie lub dodanie komponentu AudioSource
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        _body.SetActive(false);
        _explosionAnimator.Play("PoisonBomb");

        // Odtwarzamy dŸwiêk wybuchu
        PlayExplosionSound();

        StartCoroutine(ApplyPoison(zombieHealth));
    }

    protected override void OnGroundCollision()
    {
        base.OnGroundCollision();
        _body.SetActive(false);
        _explosionAnimator.Play("PoisonBomb");

        // Odtwarzamy dŸwiêk wybuchu
        PlayExplosionSound();

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

    // Odtwarzanie dŸwiêku wybuchu
    private void PlayExplosionSound()
    {
        if (explosionSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(explosionSound); // Odtwarza dŸwiêk wybuchu
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
