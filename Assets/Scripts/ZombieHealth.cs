using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maksymalny poziom ¿ycia
    public float currentHealth;    // Aktualny poziom ¿ycia
    public Image HealthBar;        // Odnoœnik do UI Image reprezentuj¹cego zdrowie

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Funkcja wywo³ywana, gdy zombie otrzymuje obra¿enia
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Funkcja wywo³ywana, gdy zombie zbiera apteczkê
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Aktualizacja paska zdrowia na UI
    void UpdateHealthBar()
    {
        HealthBar.fillAmount = currentHealth / maxHealth;
    }

    // Funkcja œmierci
    void Die()
    {
        // Dodaj logikê œmierci, np. animacjê, przejœcie do ekranu œmierci
        Debug.Log("Zombie is Dead");
    }
}
