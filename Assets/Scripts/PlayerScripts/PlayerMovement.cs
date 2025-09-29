using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    bool visualiseRaycast = false;
    [SerializeField]
    Vector2 raycastOffset = Vector2.zero;
    [SerializeField]
    Transform raycastOrigin;

    PlayerJump playerJump;

    bool isMoving = false;

    Rigidbody2D rb2d;
    [Space]
    [Space, SerializeField]
    float movementSpeed = 5f;
    [SerializeField]
    float maxSpeed = 100f;
    [SerializeField, Header("Max Animation Speed")]
    float maxAnimationSpeed = 3f;
    [SerializeField, Header("Min Animation Speed")]
    float minAnimationSpeed = 0.2f;
    [Space, SerializeField, Header("The multiplier for when not on ground.")]
    float airMovementMultiplier = 0.5f;

    [SerializeField, Header("The distance the raycast checks for a slope")]
    float slopeCheckDistance = 0.1f;
    [SerializeField]
    float slopeForce = 1f;
    float speedMultiplier = 1f;    
    Vector2 input;

    [SerializeField]
    Animator animator;

    // [SerializeField, Header("THE DISTANCE THE RAYCAST CHECKS FOR A WALL")]
    // float wallCheckDistance = 1f;
    [SerializeField, Header("The raycast distance for the floor check")]
    float floorCheckDistance = 1f;

    [SerializeField, Header("THE LAYER THAT THE WALL IS ON")]
    LayerMask layerMask;

    PlayerStunned playerStunned;

    SpringJoint2D towSpringJoint2D;

    [SerializeField]
    LineRenderer towLineRenderer;
    Vector2 towLineRendererScale = Vector2.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerJump = GetComponent<PlayerJump>();
        rb2d = GetComponent<Rigidbody2D>();
        
        if (transform.childCount > 0)
        {
            towSpringJoint2D = transform.GetChild(0).GetComponent<SpringJoint2D>();
        }

        playerStunned = GetComponent<PlayerStunned>();

        if (towLineRenderer != null)
        {
            towLineRendererScale = towLineRenderer.textureScale;
            towLineRenderer.textureScale = new Vector2(towLineRendererScale.x, -towLineRendererScale.y);
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D floorHit = Physics2D.Raycast(transform.position, -Vector2.up, floorCheckDistance, layerMask);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, -transform.right, slopeCheckDistance, layerMask);

        if (floorHit)
        {
            // Smoothly rotate to align with the floor normal
            float targetAngle = Mathf.Atan2(floorHit.normal.y, floorHit.normal.x) * Mathf.Rad2Deg - 90f;
            float smoothedAngle = Mathf.LerpAngle(rb2d.rotation, targetAngle, 0.2f);
            rb2d.MoveRotation(smoothedAngle);
        }

        if (playerStunned.IsStunned())
        {
            isMoving = false;
            animator.SetBool("Walking", false);
            return;
        }

        DrawDebugRays(floorHit);

        if (hit.collider != null && isMoving)
        {
            rb2d.AddForce(slopeForce * transform.up, ForceMode2D.Impulse);
        }

        if (isMoving)
        {
            speedMultiplier = playerJump.isGrounded() ? 1f : airMovementMultiplier;
            Vector3 projectedOnGround = Vector3.ProjectOnPlane(input, floorHit.normal).normalized;
            rb2d.AddForce((Vector2)(movementSpeed * speedMultiplier * projectedOnGround), ForceMode2D.Force);
            rb2d.linearVelocityX = Mathf.Clamp(rb2d.linearVelocityX, -maxSpeed, maxSpeed);
            transform.localScale = new Vector3(input.x > 0 ? -1 : 1, 1, 1);
            if (towLineRenderer != null)
            {
                towLineRenderer.textureScale = new Vector2(1, input.x > 0 ? towLineRendererScale.y : -towLineRendererScale.y);
            }
            if (animator != null)
            {
                animator.SetBool("Walking", floorHit.collider != null ? true : false);
                animator.speed = Mathf.Clamp(Mathf.Abs(rb2d.linearVelocityX), minAnimationSpeed, maxAnimationSpeed);
            }
            return;
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        // if (Time.timeScale == 0)
        // {
        //     input = Vector2.zero;
        //     isMoving = false;
        //     animator.SetBool("Walking", false);
        //     return;
        // }

        input = context.ReadValue<Vector2>();
        // If the input is cancelled, turn isMoving to false and set the x velocity to 0.
        if (context.canceled)
        {
            isMoving = false;
            if (animator != null)
            {
                animator.SetBool("Walking", false);
            }
            return;
        }

        if (context.started)
        {
            isMoving = true;
        }
    }

    void DrawDebugRays(RaycastHit2D floorHit)
    {
        if (!visualiseRaycast) return;
        
        Debug.DrawRay(transform.position, -transform.up * floorCheckDistance, Color.green, 1f);
        Debug.DrawRay(raycastOrigin.position, -transform.right * slopeCheckDistance, Color.red, 1f);

        if (floorHit.collider != null) Debug.DrawRay(floorHit.point, floorHit.normal * 4f, Color.blue, 1f);
    }

}