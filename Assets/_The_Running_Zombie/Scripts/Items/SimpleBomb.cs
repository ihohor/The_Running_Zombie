using UnityEngine;

public class SimpleBomb : Item
{
	[SerializeField] private Animator _explosionAnimator;
	[SerializeField] private GameObject _body;
	[SerializeField] private int damage = 50;

	protected override void Collide(ZombieStateAndHealth zombieHealth)
	{
		base.Collide(zombieHealth);
		_body.SetActive(false);
		_explosionAnimator.Play("ExplosionSB");
		zombieHealth.TakeDamage(damage);
	}
}