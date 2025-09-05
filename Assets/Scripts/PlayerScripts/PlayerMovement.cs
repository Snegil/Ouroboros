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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (isMoving)
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
            rb2d.linearVelocityX = 0;
            return;
        }
        if (context.started)
        {
            isMoving = true;
            transform.localScale = input.x > 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        }
    }
}