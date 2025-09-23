using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField]
    List<MonoBehaviour> doorToOpen = new();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerOne") || collision.gameObject.CompareTag("PlayerTwo") || collision.gameObject.CompareTag("PropTrigger"))
        {
            if (doorToOpen.Count == 0) { Debug.LogWarning(gameObject.name + "'S LIST IS EMPTY"); return; }

            foreach (var door in doorToOpen)
            {
                if (door == null)
                {
                    Debug.LogWarning("A DOOR IN " + gameObject.name + "'S LIST IS NULL");
                    break;
                }
                door.enabled = true;
            }
        }
    }
}
