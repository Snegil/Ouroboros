using System;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerJump = GetComponent<PlayerJump>();
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
}