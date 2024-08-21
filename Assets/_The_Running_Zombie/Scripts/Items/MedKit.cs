using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private int _healAmount = 50;

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        zombieHealth.Heal(_healAmount);

        // Przywracanie normalnej pr�dko�ci zombie
        ZombieMovement zombieMovement = zombieHealth.GetComponent<ZombieMovement>();
        if (zombieMovement != null)
        {
            // Za��my, �e normalna pr�dko�� to warto�� `zombieMovement.speed`, kt�ra by�a przed spowolnieniem
            float originalSpeed = zombieMovement.speed / 0.5f;  // Je�li pr�dko�� by�a spowolniona o po�ow�, to przywr�� j� do normy
            zombieMovement.speed = originalSpeed;
        }
    }
}
