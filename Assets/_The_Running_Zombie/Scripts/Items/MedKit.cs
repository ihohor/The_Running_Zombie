using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private int _healAmount = 50;

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        zombieHealth.Heal(_healAmount);
    }
}
