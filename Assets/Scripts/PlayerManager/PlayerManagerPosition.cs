using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerManagerPosition : MonoBehaviour
{
    PlayerManager playerManager;
    Rigidbody2D rb2d;

    [SerializeField]
    float moveSpeed = 5f;
    // [SerializeField]
    // float rotateSpeed = 10f;

    CapsuleCollider2D playerManagerCollider;

    [SerializeField]
    AnimationCurve sizeCurve;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        playerManagerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //Vector2 direction = (transform.position - playerManager.GetPlayerTwo().transform.position).normalized;
        //transform.right = direction;
        // Calculate the direction vector from shooter to target
        Vector3 direction = transform.position - playerManager.GetPlayerTwo().transform.position;

        // Calculate the angle needed to rotate the shooter to face the target
        // This assumes your sprites are oriented to face right or up by default
        // and you want to rotate around the Z-axis (the camera's forward axis)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation around the Z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle); // Subtract 90 if sprite faces upwards by default
        if (angle > 90 || angle < -90)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }
    void FixedUpdate()
    {
        //transform.position = playerManager.AveragePosition();

        rb2d.MovePosition(Vector2.Lerp(transform.position, playerManager.AveragePosition(), moveSpeed * Time.fixedDeltaTime));

        if (playerManager.IsJoint)
        {
            playerManagerCollider.size = new Vector2(sizeCurve.Evaluate(playerManager.DistanceBetweenPlayers()), playerManagerCollider.size.y);
        }
    }
}
