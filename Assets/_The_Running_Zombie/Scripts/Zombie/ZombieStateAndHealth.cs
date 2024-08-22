using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZombieStateAndHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    private bool shieldActive = false;
    private Animator animator;
    private ZombieMovement zombieMovement;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        animator = GetComponent<Animator>();
        zombieMovement = GetComponent<ZombieMovement>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead || shieldActive) return;

        currentHealth -= damage;
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
            animator.SetTrigger("Dead");
            zombieMovement.enabled = false;
            Invoke("LoadDeathScreen", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void LoadDeathScreen()
    {
        SceneManager.LoadScene("DeathScreenScene");
    }

    public void ActivateShield()
    {
        shieldActive = true;
        Debug.Log("Tarcza aktywna!");
    }

    public void DeactivateShield()
    {
        shieldActive = false;
        Debug.Log("Tarcza dezaktywowana!");
    }
}
