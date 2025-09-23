using UnityEngine;

public class PlayerGroundClearance : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField, Header("The distance the raycast checks for ground")]
    float groundCheckDistance = 1f;
    [SerializeField, Header("The layer that the ground is on")]
    LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, layerMask);

        if (hit.collider == null) return;
        
        if (hit.distance > groundCheckDistance)
        {
            rb2d.gravityScale = 1f;
            return;
        }
        if (hit.distance < groundCheckDistance)
        {
            rb2d.gravityScale = 0f;
        }
    }
}
