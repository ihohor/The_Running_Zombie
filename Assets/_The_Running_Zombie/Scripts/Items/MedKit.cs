using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private int _healAmount = 50;

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        zombieHealth.Heal(_healAmount);

        // Przywracanie normalnej prêdkoœci zombie
        ZombieMovement zombieMovement = zombieHealth.GetComponent<ZombieMovement>();
        if (zombieMovement != null)
        {
            // Za³ó¿my, ¿e normalna prêdkoœæ to wartoœæ `zombieMovement.speed`, która by³a przed spowolnieniem
            float originalSpeed = zombieMovement.speed / 0.5f;  // Jeœli prêdkoœæ by³a spowolniona o po³owê, to przywróæ j¹ do normy
            zombieMovement.speed = originalSpeed;
        }
    }
}
