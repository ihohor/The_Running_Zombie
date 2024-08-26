using UnityEngine;
using System.Collections;

public class SimpleBomb : Item
{
    [SerializeField] private Animator _explosionAnimator;
    [SerializeField] private GameObject _body;
    [SerializeField] private int damage = 50;
    [SerializeField] private float explosionRadius = 5f;

    // Dodanie pola d�wi�ku, kt�re b�dzie widoczne w inspektorze
    [SerializeField] private AudioClip explosionSound;

    private AudioSource _audioSource;

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
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        _body.SetActive(false);
        _explosionAnimator.Play("ExplosionSB");

        // Odtwarzanie d�wi�ku wybuchu
        PlayExplosionSound();

        zombieHealth.TakeDamage(damage);
    }

    protected override void OnGroundCollision()
    {
        base.OnGroundCollision();
        _body.SetActive(false);
        _explosionAnimator.Play("ExplosionSB");

        // Odtwarzanie d�wi�ku wybuchu
        PlayExplosionSound();

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
