using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 5f;

    private bool _ishasCollided;

    private void Start()
    {
        StartCoroutine(DestroyWithDelay());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            OnGroundCollision();
        }
        else if (collision.collider.TryGetComponent(out ZombieStateAndHealth health) && !_ishasCollided)
        {
            Collide(health);
        }
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

    protected virtual void OnGroundCollision()
    {
        Debug.Log("Kolizja z ziemia!");
    }
}

