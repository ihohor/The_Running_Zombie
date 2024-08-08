using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool move = false;
    public float jumpForce = 10.0f;
    public bool Dead = false; // Nowa zmienna dla stanu �mierci

    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float lastTapTime;
    private float doubleTapTime = 0.3f; // Czas pomi�dzy dwoma dotkni�ciami
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!Dead) // Sprawdzenie czy posta� nie jest martwa
        {
            HandleTouchInput();

            // Ustawienie parametru Speed w Animatorze
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            // Odwracanie postaci w zale�no�ci od kierunku ruchu
            if (rb.velocity.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (rb.velocity.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Zatrzymanie ruchu, je�li posta� jest martwa
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0 && !Dead) // Tylko gdy posta� nie jest martwa
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x > Screen.width / 2)
                {
                    // Prawa strona ekranu - posta� idzie w prawo
                    MoveRight();
                    move = true;
                }
                else
                {
                    // Lewa strona ekranu - posta� idzie w lewo
                    MoveLeft();
                    move = true;
                }

                // Sprawdzenie podw�jnego dotkni�cia
                if (Time.time - lastTapTime < doubleTapTime)
                {
                    Jump();
                }

                lastTapTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StopMoving();
                move = false;
            }
        }
    }

    void MoveRight()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void MoveLeft()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Obs�uga wykrywania l�dowania
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Kolizja z bomb�
        if (collision.collider.CompareTag("Bomb"))
        {
            Dead = true; // Ustawienie stanu na martwego
            animator.SetTrigger("Dead"); // Uruchomienie animacji �mierci
            // Przej�cie do ekranu �mierci po zako�czeniu animacji
            Invoke("LoadDeathScreen", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void LoadDeathScreen()
    {
        SceneManager.LoadScene("DeathScreenScene"); // Upewnij si�, �e nazwa sceny jest poprawna
    }
}
