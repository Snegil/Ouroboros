using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputChanger : MonoBehaviour
{
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] private GameObject player2Prefab;

    // Start is called before the first frame update
    void Start()
    {
       // playerInputManager = GetComponent<PlayerInputManager>();
    }

   public void OnPlayerJoin(PlayerInput player)
    {
        playerInputManager.playerPrefab = player2Prefab;
    }
}
