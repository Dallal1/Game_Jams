using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement 
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // Ground Check
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // Private variables
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. تحقق: هل اللاعب على الأرض؟
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 2. الحركة الأفقية
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _rigidbody.linearVelocity = new Vector2(horizontalInput * moveSpeed, _rigidbody.linearVelocity.y);

        // 3. القفز (بس لو على الأرض)
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, jumpForce);
        }
    }
}
