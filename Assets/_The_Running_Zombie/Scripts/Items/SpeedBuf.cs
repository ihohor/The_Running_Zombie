using System.Collections;
using UnityEngine;

public class SpeedBuf : Bufs
{
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float buffDuration = 5f;

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        ZombieMovement zombieMovement = zombieHealth.GetComponent<ZombieMovement>();

        if (zombieMovement != null)
        {
            Debug.Log("Buff na pr�dko�� zosta� zebrany. Aktualna pr�dko��: " + zombieMovement.speed);
            StartCoroutine(ApplySpeedBuff(zombieMovement));
        }

        Destroy(gameObject);
    }

    private IEnumerator ApplySpeedBuff(ZombieMovement zombieMovement)
    {
        float originalSpeed = zombieMovement.speed;
        zombieMovement.speed *= speedMultiplier; 
        Debug.Log("Pr�dko�� zombie po zebraniu buffa: " + zombieMovement.speed);

        yield return new WaitForSeconds(buffDuration); 

        zombieMovement.speed = originalSpeed;
        Debug.Log("Pr�dko�� zombie po zako�czeniu buffa: " + zombieMovement.speed);
    }

    
    protected override void OnGroundCollision()
    {
        base.OnGroundCollision();
        Destroy(gameObject);
    }
}

