using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZombieStateAndHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public bool shieldActive = false;
    private Animator animator;
    private ZombieMovement zombieMovement;
    private bool isDead = false;

    // Dodane dŸwiêki
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource _audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        animator = GetComponent<Animator>();
        zombieMovement = GetComponent<ZombieMovement>();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TakeDamage(float damage)
    {

        currentHealth -= damage;
        PlayDamageSound(); // Odtwórz dŸwiêk obra¿eñ

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            animator.Play("Dead");
            PlayDeathSound(); // Odtwórz dŸwiêk œmierci
            zombieMovement.enabled = false;
            Invoke("LoadDeathScreen", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void LoadDeathScreen()
    {
        SceneManager.LoadScene("DeathScreenScene");
    }

    // Funkcje do odtwarzania dŸwiêków
    private void PlayDamageSound()
    {
        if (damageSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(damageSound); // Odtwórz dŸwiêk obra¿eñ
        }
    }

    private void PlayDeathSound()
    {
        if (deathSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(deathSound); // Odtwórz dŸwiêk œmierci
        }
    }
}
