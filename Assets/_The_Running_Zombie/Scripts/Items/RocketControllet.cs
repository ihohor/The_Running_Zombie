using UnityEngine;
using System.Collections;

public class HomingRocket : MonoBehaviour
{
    [SerializeField] private Animator _explosionAnimator;
    [SerializeField] private GameObject _body;
    [SerializeField] private int damage = 50;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 200f;

    private Transform _target;

    private void Start()
    {
        // Znajduje obiekt gracza za pomoc� tagu "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _target = player.transform;
        }
    }

    private void Update()
    {
        if (_target != null)
        {
            // Oblicza kierunek w stron� gracza
            Vector2 direction = (Vector2)_target.position - (Vector2)transform.position;
            direction.Normalize();

            // Obraca rakiet� w stron� celu
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            transform.Rotate(0, 0, -rotateAmount * rotationSpeed * Time.deltaTime);

            // Przemieszcza rakiet� w kierunku gracza
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdza, czy rakieta uderzy�a w gracza
        if (collision.CompareTag("Player"))
        {
            Explode();
            // Mo�esz doda� logik� uszkadzania gracza tutaj
        }
        // Sprawdza, czy rakieta uderzy�a w zombie
        else if (collision.TryGetComponent<ZombieStateAndHealth>(out ZombieStateAndHealth zombieHealth))
        {
            Explode();
            zombieHealth.TakeDamage(damage);
        }
    }

    private void Explode()
    {
        _body.SetActive(false);
        _explosionAnimator.Play("ExplosionSB");

        StartCoroutine(ApplyExplosionDamage());
        StartCoroutine(DestroyAfterExplosion());
    }

    private IEnumerator ApplyExplosionDamage()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Znajduje wszystkie obiekty w promieniu eksplozji
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out ZombieStateAndHealth zombieHealth))
            {
                zombieHealth.TakeDamage(damage);
            }
            // Mo�na doda� logik� uszkadzania innych obiekt�w np. gracza
        }
    }

    private IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(_explosionAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Rysuje wizualne przedstawienie zasi�gu eksplozji w edytorze
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
