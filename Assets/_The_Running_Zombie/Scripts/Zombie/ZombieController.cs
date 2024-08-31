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

    [SerializeField] private Animator _animationComtrole;
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip idleSound;
    [SerializeField] private AudioClip jumpSound;
    private AudioSource _audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3;
        _animationComtrole.Play("Idle");

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
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

        if (rb.velocity.x == 0 && isGrounded)
        {
            PlayIdleSound();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Deklaracja zmiennej 'touch'

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x > Screen.width / 2)
                {
                    MoveRight();
                    _animationComtrole.Play("Walk");
                }
                else if (touch.position.x <= Screen.width / 2)
                {
                    MoveLeft();
                    _animationComtrole.Play("Walk");
                }

                // Dodaj logik� skoku w odpowiednim miejscu
                if (touch.position.y < Screen.height / 4 && isGrounded) // Skok, gdy dotkni�cie jest w dolnej cz�ci ekranu
                {
                    Jump();
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StopMoving();
                _animationComtrole.Play("Idle");
            }
        }
    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (!facingRight)
        {
            Flip();
        }

        PlayWalkingSound();
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        if (facingRight)
        {
            Flip();
        }

        PlayWalkingSound();
    }

    private void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        _audioSource.Stop();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            PlayJumpSound();
        }
    }

    private void Flip()
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

    private void PlayWalkingSound()
    {
        if (walkingSound != null && _audioSource != null && !_audioSource.isPlaying)
        {
            _audioSource.clip = walkingSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    private void PlayIdleSound()
    {
        if (idleSound != null && _audioSource != null && !_audioSource.isPlaying)
        {
            _audioSource.clip = idleSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    private void PlayJumpSound()
    {
        if (jumpSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(jumpSound);
        }
    }
}
