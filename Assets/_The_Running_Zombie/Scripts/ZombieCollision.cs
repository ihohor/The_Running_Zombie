using UnityEngine;

public class ZombieCollision : MonoBehaviour
{
    private ZombieStateAndHealth ZombieStateAndHealth;

    void Awake()
    {
        ZombieStateAndHealth = GetComponent<ZombieStateAndHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("MedKit"))
        {
            ZombieStateAndHealth.Heal(20f);
            Destroy(collision.gameObject);
        }
    }
}
