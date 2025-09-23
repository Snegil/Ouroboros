using UnityEngine;

public class DoorButtonChildConstraints : MonoBehaviour
{
    Rigidbody2D rb2d;

    DoorButtonSpecificPlayer doorButtonSpecificPlayer;

    bool doneOnce = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        doorButtonSpecificPlayer = transform.parent.GetComponent<DoorButtonSpecificPlayer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (doneOnce) return;

        if (collision.gameObject.CompareTag(doorButtonSpecificPlayer.Player.tag))
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            doneOnce = true;
        }
    }
}
