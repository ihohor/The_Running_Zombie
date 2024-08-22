using System.Collections;
using UnityEngine;

public class ShieldBuf : Bufs
{
    [SerializeField] private GameObject shieldPrefab; 
    [SerializeField] private float shieldDuration = 10f;

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);

        if (zombieHealth != null)
        {
            Debug.Log("Buff tarczy energetycznej zosta³ zebrany.");

           
            StartCoroutine(ApplyShield(zombieHealth));
        }

        Destroy(gameObject);
    }

    private IEnumerator ApplyShield(ZombieStateAndHealth zombieHealth)
    {
        GameObject shield = Instantiate(shieldPrefab, zombieHealth.transform.position, Quaternion.identity);
        shield.transform.SetParent(zombieHealth.transform);

        zombieHealth.ActivateShield();

        yield return new WaitForSeconds(shieldDuration);

        zombieHealth.DeactivateShield();

        Destroy(shield);
    }

    protected override void OnGroundCollision()
    {
        base.OnGroundCollision();
        Destroy(gameObject);
    }
}
