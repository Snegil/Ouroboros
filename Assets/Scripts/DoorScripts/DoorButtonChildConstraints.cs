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
        targetJoint2D.frequency = targetFrequencyWhenReleased;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || collision.gameObject == null) return;
        if (doorButtonSpecificPlayer == null || doorButtonSpecificPlayer.Player == null) return;
        if (targetJoint2D == null) return;

        try
        {
            if (!collision.gameObject.CompareTag(doorButtonSpecificPlayer.Player.tag)) return;
            
            targetJoint2D.frequency = targetFrequencyWhenPressed;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Error in OnTriggerEnter2D: {e.Message}");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null || collision.gameObject == null) return;
        if (doorButtonSpecificPlayer == null || doorButtonSpecificPlayer.Player == null) return;
        if (targetJoint2D == null) return;

        try
        {
            if (!collision.gameObject.CompareTag(doorButtonSpecificPlayer.Player.tag)) return;
            
            targetJoint2D.frequency = targetFrequencyWhenReleased;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Error in OnTriggerExit2D: {e.Message}");
        }
    }
}