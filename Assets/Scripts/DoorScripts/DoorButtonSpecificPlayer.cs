using System.Collections.Generic;
using UnityEngine;

public class DoorButtonSpecificPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public GameObject Player { get { return player; } }

    [SerializeField]
    List<MonoBehaviour> doorToOpen = new();

    void Start()
    {
        // Clean up any null references in the list at start
        CleanDoorList();
    }

    void CleanDoorList()
    {
        doorToOpen.RemoveAll(door => door == null);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || player == null)
        {
            return;
        }

        if (doorToOpen.Count == 0) 
        { 
            Debug.LogWarning(gameObject.name + "'S LIST IS EMPTY"); 
            return; 
        }

        if (collision.gameObject.CompareTag(player.tag))
        {
            try
            {
                foreach (var door in doorToOpen)
                {
                    // Check if the door component still exists before accessing it
                    if (door != null && door.gameObject != null)
                    {
                        door.enabled = true;
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Error enabling doors in {gameObject.name}: {e.Message}");
            }
        }
    }
}
