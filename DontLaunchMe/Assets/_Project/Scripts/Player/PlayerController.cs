using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ===== Movement =====
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // ===== Ground Check =====
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // ===== Wall Check =====
    public Transform wallCheck;
    public float wallCheckRadius = 0.2f;
    public LayerMask wallLayer;

    // ===== Wall Jump =====
    public float wallSlideSpeed = 2f;
    public Vector2 wallJumpForce = new Vector2(8f, 12f);
    public float wallJumpDuration = 0.2f;

    // ===== Slide =====
    public float slideSpeed = 10f;
    public float slideDuration = 0.5f;

    // ===== Private =====
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isWallSliding;
    private bool _isWallJumping;
    private float _wallJumpTimer;
    private int _facingDirection = 1;
    private bool _isSliding;
    private float _slideTimer;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckSurroundings();
        HandleMovement();
        HandleJump();
        HandleWallSlide();
        HandleWallJump();
        HandleSlide();
    }

    // نتحقق من الأرض والحيط
    void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        _isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }

    // الحركة العادية
    void HandleMovement()
    {
        // لو بيعمل wall jump، ما نتحكمش في الحركة مؤقتاً
        if (_isWallJumping) return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _rigidbody.linearVelocity = new Vector2(horizontalInput * moveSpeed, _rigidbody.linearVelocity.y);

        // نغير الاتجاه اللي بيبص فيه
        if (horizontalInput > 0) _facingDirection = 1;
        else if (horizontalInput < 0) _facingDirection = -1;

        // لا نغير الـ Scale لو بيعمل Slide
        if (!_isSliding)
        {
            // نقلب اللاعب بصرياً
            transform.localScale = new Vector3(_facingDirection, 1, 1);
        }
    }

    // القفز العادي
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, jumpForce);
        }
    }

    // الانزلاق على الحيط
    void HandleWallSlide()
    {
        // لو بيلمس حيط وطاير (مش على الأرض)
        if (_isTouchingWall && !_isGrounded && _rigidbody.linearVelocity.y < 0)
        {
            _isWallSliding = true;
            // نبطئ النزول
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, -wallSlideSpeed);
        }
        else
        {
            _isWallSliding = false;
        }
    }

    // القفز من الحيط
    void HandleWallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isWallSliding)
        {
            _isWallJumping = true;
            _wallJumpTimer = wallJumpDuration;

            // نقفز في الاتجاه المعاكس للحيط
            _rigidbody.linearVelocity = new Vector2(-_facingDirection * wallJumpForce.x, wallJumpForce.y);
        }

        // نعد الوقت
        if (_isWallJumping)
        {
            _wallJumpTimer -= Time.deltaTime;
            if (_wallJumpTimer <= 0) _isWallJumping = false;
        }
    }

    // الانزلاق تحت
    void HandleSlide()
    {
        // نبدأ Slide لو دايس تحت وعلى الأرض ومش بيعمل slide أصلاً
        if (Input.GetKeyDown(KeyCode.S) && _isGrounded && !_isSliding && Mathf.Abs(_rigidbody.linearVelocity.x) > 0.1f)
        {
            _isSliding = true;
            _slideTimer = slideDuration;

            // نصغر ارتفاع اللاعب
            transform.localScale = new Vector3(_facingDirection, 0.5f, 1);

            // نديه سرعة إضافية
            _rigidbody.linearVelocity = new Vector2(_facingDirection * slideSpeed, _rigidbody.linearVelocity.y);
        }

        // نعد الوقت
        if (_isSliding)
        {
            _slideTimer -= Time.deltaTime;
            if (_slideTimer <= 0)
            {
                _isSliding = false;
                // نرجع الحجم الطبيعي
                transform.localScale = new Vector3(_facingDirection, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }

        if (other.CompareTag("Goal"))
        {
            // نبحث عن GameManager ونستدعي PlayerWins
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.PlayerWins();
            }
        }
    }

}