using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZombieStateAndHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar; // Przypisz obrazek paska zdrowia w inspektorze

    private Animator animator;
    private ZombieMovement zombieMovement;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        UpdateHealthLog(); // Upewnij siê, ¿e poziom zdrowia jest wyœwietlany na starcie

        animator = GetComponent<Animator>();
        zombieMovement = GetComponent<ZombieMovement>();
    }

    // Metoda do zadawania obra¿eñ
    public void TakeDamage(float damage)
    {
        if (isDead) return; // Jeœli zombie ju¿ jest martwy, nie przyjmuj wiêcej obra¿eñ

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthBar();
        UpdateHealthLog(); // Wypisz poziom zdrowia do konsoli po zadaniu obra¿eñ
    }

    // Metoda do leczenia
    public void Heal(float healAmount)
    {
        if (isDead) return; // Jeœli zombie jest martwy, nie lecz go

        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
        UpdateHealthLog(); // Wypisz poziom zdrowia do konsoli po leczeniu
    }

    // Aktualizacja paska zdrowia
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    // Wypisywanie poziomu zdrowia do konsoli
    void UpdateHealthLog()
    {
        Debug.Log($"Health: {currentHealth}/{maxHealth}");
    }

    // Metoda wywo³ywana, gdy zdrowie spadnie do 0
    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            animator.SetTrigger("Dead");
            zombieMovement.enabled = false; // Wy³¹czanie skryptu ruchu
            Invoke("LoadDeathScreen", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    // Przejœcie do ekranu œmierci
    void LoadDeathScreen()
    {
        SceneManager.LoadScene("DeathScreenScene"); // Upewnij siê, ¿e nazwa sceny jest poprawna
    }
}
