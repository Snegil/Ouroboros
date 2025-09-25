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

    [SerializeField]
    float stunTimer = 0.5f;
    float setStunTimer;

    SpringJoint2D towSpringJoint2D;

    Vector2 originalTowPosition;

    [SerializeField]
    LineRenderer towLineRenderer;
    Vector2 towLineRendererScale = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setStunTimer = stunTimer;
        stunTimer = 0f;

        playerJump = GetComponent<PlayerJump>();
        rb2d = GetComponent<Rigidbody2D>();
        towSpringJoint2D = transform.GetChild(0).GetComponent<SpringJoint2D>();
        originalTowPosition = towSpringJoint2D.connectedAnchor;

        towLineRendererScale = towLineRenderer.textureScale;
        towLineRenderer.textureScale = new Vector2(towLineRendererScale.x, -towLineRendererScale.y);
    }
    void FixedUpdate()
    {
        RaycastHit2D floorHit = Physics2D.Raycast(transform.position, -Vector2.up, floorCheckDistance, layerMask);
        if (visualiseRaycast) Debug.DrawRay(transform.position, -transform.up * floorCheckDistance, Color.green, 1f);
        if (visualiseRaycast && floorHit)
        {
            Debug.DrawRay(floorHit.point, floorHit.normal * 4f, Color.blue, 1f);
        }
        //transform.up = floorHit ? floorHit.normal : Vector2.up;
        if (floorHit)
        {
            // Smoothly rotate to align with the floor normal
            float targetAngle = Mathf.Atan2(floorHit.normal.y, floorHit.normal.x) * Mathf.Rad2Deg - 90f;
            float smoothedAngle = Mathf.LerpAngle(rb2d.rotation, targetAngle, 0.2f);
            rb2d.MoveRotation(smoothedAngle);
        }

        if (stunTimer > 0)
        {
            stunTimer -= Time.fixedDeltaTime;
            return;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, -transform.right, slopeCheckDistance, layerMask);
        if (visualiseRaycast) Debug.DrawRay(raycastOrigin.position, -transform.right * slopeCheckDistance, Color.red, 1f);
        
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
            towLineRenderer.textureScale = new Vector2(1, input.x > 0 ? towLineRendererScale.y : -towLineRendererScale.y);
            animator.SetBool("Walking", true);
            animator.speed = Mathf.Clamp(Mathf.Abs(rb2d.linearVelocityX), minAnimationSpeed, maxAnimationSpeed);
            return;
        }
    }
    public void Movement(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
        {
            input = Vector2.zero;
            isMoving = false;
            animator.SetBool("Walking", false);
            return;
        }

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