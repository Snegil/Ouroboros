using System.Collections;
using UnityEngine;

public class DoorButtonChildConstraints : MonoBehaviour
{
    Rigidbody2D rb2d;

    DoorButtonSpecificPlayer doorButtonSpecificPlayer;

    [SerializeField]
    float delay = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        doorButtonSpecificPlayer = transform.parent.GetComponent<DoorButtonSpecificPlayer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(doorButtonSpecificPlayer.Player.tag))
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(doorButtonSpecificPlayer.Player.tag))
        {
            StartCoroutine(WaitToFreeze());
        }        
    }

    IEnumerator WaitToFreeze()
    {
        yield return new WaitForSeconds(delay);
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
}