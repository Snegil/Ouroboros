using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField]
    List<MonoBehaviour> doorToOpen = new();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerOne") || !collision.gameObject.CompareTag("PlayerTwo"))
        {
            foreach (var door in doorToOpen)
            {
                door.enabled = true;
            }
        }
    }
}
