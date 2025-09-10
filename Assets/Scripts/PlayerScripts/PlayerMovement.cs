using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    PlayerJump playerJump;

    bool isMoving = false;

    Rigidbody2D rb2d;

    [SerializeField]
    float movementSpeed = 5f;

    Vector2 input;

    [SerializeField]
    Animator animator;

    [SerializeField, Header("THE DISTANCE THE RAYCAST CHECKS FOR A WALL")]
    float wallCheckDistance = 1f;
    [SerializeField, Header("THE LAYER THAT THE WALL IS ON")]
    LayerMask layerMask;

    [SerializeField]
    float stunTimer = 0.5f;
    float setStunTimer;

    SpringJoint2D towSpringJoint2D;
    DistanceJoint2D towDistanceJoint2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setStunTimer = stunTimer;

        playerJump = GetComponent<PlayerJump>();
        rb2d = GetComponent<Rigidbody2D>();
        towSpringJoint2D = transform.GetChild(0).GetComponent<SpringJoint2D>();
    }
    void FixedUpdate()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.fixedDeltaTime;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, wallCheckDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.right * wallCheckDistance, Color.red, 1f);
        Debug.Log(hit.collider + " " + hit.normal.x + " " + input.normalized.x + " " + Vector2.SqrMagnitude(hit.normal - input.normalized));

        if (hit.collider != null && Vector2.SqrMagnitude(hit.normal - input.normalized) > 0.1f)
        {
            rb2d.linearVelocityX = 0;
            rb2d.position = new Vector2(rb2d.position.x + (hit.distance - (wallCheckDistance - 0.2f)) * -transform.right.x, rb2d.position.y);
            animator.SetBool("Walking", false);
            return;
        }
        if (isMoving)
        {
            rb2d.linearVelocityX = input.x * movementSpeed;
            animator.SetBool("Walking", true);
            transform.right = input.x > 0 ? Vector2.left : Vector2.right;
            towSpringJoint2D.connectedAnchor = input.x > 0 ? Vector2.left : Vector2.right;
            return;
        }

    }
    public void Movement(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        // If the input is cancelled, turn isMoving to false and set the x velocity to 0.
        if (context.canceled)
        {
            isMoving = false;
            animator.SetBool("Walking", false);
            return;
        }

        if (context.started)
        {
            isMoving = true;
        }
    }
    public void ActivateStunTimer()
    {
        stunTimer = setStunTimer;
        animator.SetBool("Walking", false);
    }
}