using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar; // Przypisz obrazek paska zdrowia w inspektorze

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Metoda do zadawania obra¿eñ
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthBar();
    }

    // Metoda do leczenia
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    // Aktualizacja paska zdrowia
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    // Metoda wywo³ywana, gdy zdrowie spadnie do 0
    void Die()
    {
        // Mo¿esz tutaj dodaæ logikê œmierci
        Debug.Log("Zombie is Dead!");
    }
}

