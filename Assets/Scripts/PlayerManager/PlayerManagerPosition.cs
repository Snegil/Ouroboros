using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerPosition : MonoBehaviour
{
    PlayerManager playerManager;
    Rigidbody2D rb2d;

    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float rotateSpeed = 10f;

    void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = playerManager.AveragePosition();

        rb2d.MovePosition(Vector2.Lerp(transform.position, playerManager.AveragePosition(), moveSpeed * Time.fixedDeltaTime));
        Vector2 direction = playerManager.GetPlayerTwo().transform.GetChild(0).position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb2d.MoveRotation(Mathf.LerpAngle(rb2d.rotation, angle, rotateSpeed * Time.fixedDeltaTime));
    }
}
