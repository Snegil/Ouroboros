using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DoorButtonChildConstraints : MonoBehaviour
{
    TargetJoint2D targetJoint2D;

    DoorButtonSpecificPlayer doorButtonSpecificPlayer;

    [SerializeField]
    float targetFrequencyWhenPressed = 5f;
    [SerializeField]
    float targetFrequencyWhenReleased = 500f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetJoint2D = GetComponent<TargetJoint2D>();
        doorButtonSpecificPlayer = transform.parent.GetComponent<DoorButtonSpecificPlayer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }

        if (collision.gameObject.CompareTag(doorButtonSpecificPlayer.Player.tag))
        {
            targetJoint2D.frequency = targetFrequencyWhenPressed;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }

        if (collision.gameObject.CompareTag(doorButtonSpecificPlayer.Player.tag))
        {
            targetJoint2D.frequency = targetFrequencyWhenReleased;
        }
    }
}