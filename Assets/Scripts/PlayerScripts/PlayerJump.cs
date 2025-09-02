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

    [SerializeField]
    float coyoteTime;
    float setCoyoteTime;

    bool startCoyoteTime;

    RaycastHit2D hit;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        setCoyoteTime = coyoteTime;
    }
    void Update()
    {

        if (!startCoyoteTime) { return; }

        coyoteTime -= Time.deltaTime;

        if (coyoteTime <= 0)
        {
            hit = Physics2D.Raycast(transform.position, -transform.up, groundCheckDistance, layerMask);
            if (hit.collider != null)
            {
                Jumping();
                startCoyoteTime = false;
                coyoteTime = setCoyoteTime;
            }
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        hit = Physics2D.Raycast(transform.position, -transform.up, groundCheckDistance, layerMask);
        //Debug.DrawRay(transform.position, -transform.up * groundCheckDistance, Color.red, 1f);
        if (context.canceled) return;
        if (context.started && hit.collider != null)
        {
            Jumping();
        }
        if (context.started && hit.collider == null)
        {
            coyoteTime = setCoyoteTime;
            startCoyoteTime = true;           
        }
    }

    void Jumping()
    {
        rb2d.linearVelocityY = jumpForce;
    }
}