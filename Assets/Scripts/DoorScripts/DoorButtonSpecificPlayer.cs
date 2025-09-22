using System.Collections.Generic;
using UnityEngine;

public class DoorButtonSpecificPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public GameObject Player { get { return player; } }

    [SerializeField]
    List<MonoBehaviour> doorToOpen = new();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(player.tag))
        {
            foreach (var door in doorToOpen)
            {
                door.enabled = true;
            }
        }
    }
}
