using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Gravity))]
public class PlayerMovement : MonoBehaviour
{
    bool isMoving = false;

    Rigidbody2D rb2d;

    [SerializeField]
    float movementSpeed = 5f;

    Vector2 input;
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
        }
        
    }
    public void Movement(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        // If the input is cancelled, turn isMoving to false and set the x velocity to 0.
        if (context.canceled)
        {
            isMoving = false;
            rb2d.linearVelocityX = 0;
            return;
        }
        if (context.started)
        {
            isMoving = true;
        }
    }
}