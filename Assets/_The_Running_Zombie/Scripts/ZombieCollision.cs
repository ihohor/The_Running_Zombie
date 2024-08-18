using UnityEngine;

public class ZombieCollision : MonoBehaviour
{
	private ZombieStateAndHealth _zombieStateAndHealth;

	void Awake()
	{
		_zombieStateAndHealth = GetComponent<ZombieStateAndHealth>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.TryGetComponent(out MedKit medKit))
		{
			_zombieStateAndHealth.Heal(20f);
			Destroy(collision.gameObject);
			print("medkit");
		}
	}
}
