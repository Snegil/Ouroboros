using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 5f;
    [SerializeField]
    float groundCheckDistance = 1f;
    [SerializeField]
    LayerMask layerMask;

    Rigidbody2D rb2d;

    RaycastHit2D hit;

    [SerializeField]
    Animator animator;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }
    public RaycastHit2D GroundCheck()
    {
        hit = Physics2D.Raycast(transform.position, -transform.up, groundCheckDistance, layerMask);
        return hit;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.DrawRay(transform.position, -transform.up * groundCheckDistance, Color.red, 1f);
        if (context.canceled) return;
        if (context.started && GroundCheck().collider != null)
        {
            Jumping();
            animator.SetTrigger("Jump");
        }
    }

    void Jumping()
    {
        rb2d.linearVelocityY = jumpForce;
    }
}