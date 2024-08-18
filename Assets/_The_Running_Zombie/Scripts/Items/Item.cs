using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	private bool _ishasCollided;
	private float _destroyDelay = 2f;

	private void Start()
	{
		StartCoroutine(DestroyWithDelay());
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.TryGetComponent(out ZombieStateAndHealth health) && !_ishasCollided)
			Collide(health);
	}

	protected virtual void Collide(ZombieStateAndHealth zombieHealth)
	{
		_ishasCollided = true;
	}

	private IEnumerator DestroyWithDelay()
	{
		yield return new WaitForSeconds(_destroyDelay);
		Destroy(gameObject);
	}
}
