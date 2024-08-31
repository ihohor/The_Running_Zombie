using UnityEngine;
using System.Collections;

public class SimpleBomb : Item
{
	[SerializeField] private Animator _explosionAnimator;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip explosionSound;
	[SerializeField] private Rigidbody2D _rigidbody;
	[SerializeField] private Collider2D _collider;
	[SerializeField] private GameObject _body;
	[SerializeField] private float explosionRadius = 5f;
	[SerializeField] private int damage = 50;

	protected override void Collide(ZombieStateAndHealth zombieHealth)
	{
		base.Collide(zombieHealth);
		DisableBomb();
		print("health");
		zombieHealth.TakeDamage(damage);
	}
	
	private void DisableBomb()
	{
		_body.SetActive(false);
		_rigidbody.bodyType = RigidbodyType2D.Static;
		_rigidbody.freezeRotation = true;
		_collider.enabled = false;
		transform.rotation = new Quaternion(0, 0, 0, 0);
		_explosionAnimator.Play("ExplosionSB");
		PlayExplosionSound();
	}

	protected override void OnGroundCollision()
	{
		base.OnGroundCollision();
		DisableBomb();
		StartCoroutine(ApplyExplosionDamage());
		StartCoroutine(DestroyAfterExplosion());
	}

	private IEnumerator ApplyExplosionDamage()
	{
		yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);

		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
		foreach (Collider2D collider in colliders)
		{
			if (collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
			{
				zombieHealth.TakeDamage(damage);
			}
		}
	}

	private IEnumerator DestroyAfterExplosion()
	{
		yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(gameObject);
	}

	private void PlayExplosionSound()
	{
		if (explosionSound != null && _audioSource != null)
		{
			_audioSource.PlayOneShot(explosionSound);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
