using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    private float lastTapTime;
    private float doubleTapTime = 0.3f; // Czas pomiêdzy dwoma dotkniêciami

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x > Screen.width / 2)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }

                if (Time.time - lastTapTime < doubleTapTime)
                {
                    Jump();
                }

                lastTapTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StopMoving();
            }
        }
    }

    void MoveRight()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (!facingRight)
        {
            Flip();
        }
    }

    void MoveLeft()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        if (facingRight)
        {
            Flip();
        }
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            float jumpDirection = facingRight ? 1 : -1;
            rb.velocity = new Vector2(jumpDirection * speed, jumpForce);
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

    // Obs³uga l¹dowania
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
