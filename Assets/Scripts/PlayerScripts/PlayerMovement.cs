using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        transform.right = input.x > 0 ? Vector2.left: Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, wallCheckDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.right * wallCheckDistance, Color.red, 1f);

        if (hit.collider != null && Vector2.Dot(hit.normal, rb2d.linearVelocity) < 0)
        {
            rb2d.linearVelocityX = 0;
            animator.SetBool("Walking", false);
            return;
        }
        if (isMoving && hit.collider == null)
        {
            rb2d.linearVelocityX = input.x * movementSpeed;
            animator.SetBool("Walking", true);
            //transform.localScale = input.x > 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            
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
            //rb2d.linearVelocityX = 0;
            return;
        }
        if (context.started)
        {
            isMoving = true;
            //transform.right = input.x > 0 ? Vector2.right : Vector2.left;
            //transform.localScale = input.x > 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            //transform.rotation = Quaternion.Euler(0, input.x > 0 ? 180 : 0, 0);
        }
    }
}