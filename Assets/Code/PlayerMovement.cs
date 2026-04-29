using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Налаштування руху")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Перевірка землі")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Звуки")]
    public AudioClip jumpSound;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private AudioSource audioSource;

    private int extraJumps;
    public int extraJumpsValue = 1;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Анімація
        if (horizontalInput != 0)
        {
            anim.SetFloat("speed", 1f);
            sprite.flipX = horizontalInput < 0;
        }
        else anim.SetFloat("speed", 0f);

        // Стрибки
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded) extraJumps = extraJumpsValue;

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded) Jump();
            else if (extraJumps > 0)
            {
                Jump();
                extraJumps--;
            }
        }
    }

    private void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
        if (jumpSound != null) audioSource.PlayOneShot(jumpSound);
    }
}