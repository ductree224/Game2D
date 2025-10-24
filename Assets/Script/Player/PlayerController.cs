using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rb;

    [Header("Player Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontalInput;

    private void FixedUpdate() {
        if (rb == null) return;

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

    }

    #region PlAYER_CONTROLS
    public void Move(InputAction.CallbackContext context) {
        horizontalInput = context.ReadValue<Vector2>().x;

        if (horizontalInput > 0.1f) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (horizontalInput < -0.1f) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    #endregion
}
