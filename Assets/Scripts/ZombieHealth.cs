using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maksymalny poziom �ycia
    public float currentHealth;    // Aktualny poziom �ycia
    public Image HealthBar;        // Odno�nik do UI Image reprezentuj�cego zdrowie

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Funkcja wywo�ywana, gdy zombie otrzymuje obra�enia
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

    // Funkcja wywo�ywana, gdy zombie zbiera apteczk�
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

    // Funkcja �mierci
    void Die()
    {
        // Dodaj logik� �mierci, np. animacj�, przej�cie do ekranu �mierci
        Debug.Log("Zombie is Dead");
    }
}
