using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public bool IsSlowed = false;
    private float slowTimeLeft;

    public float jumpForce = 10.0f;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    private float lastTapTime;
    private float doubleTapTime = 0.3f;
    public Transform groundCheck; 
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3; 
    }

    void Update()
    {
        HandleTouchInput();

        float clampedX = Mathf.Clamp(transform.position.x, -10f, 10f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        if (IsSlowed)
        {
            slowTimeLeft -= Time.deltaTime;
            if (slowTimeLeft <= 0)
            {
                IsSlowed = false;
                speed = 5.0f;
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void ExtendSlowDuration(float additionalDuration)
    {
        slowTimeLeft += additionalDuration;
        IsSlowed = true;
        speed = 5.0f / 2;
    }

    public void ResetSpeed()
    {
        speed = 5.0f;
    }
}
