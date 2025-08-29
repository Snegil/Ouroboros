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

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, groundCheckDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.up * groundCheckDistance, Color.red, 1f);
        if (context.canceled || !hit) return;
        if (context.started)
        {
            rb2d.linearVelocityY = jumpForce;
        }        
    }
}