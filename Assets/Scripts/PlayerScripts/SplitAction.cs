using UnityEngine;
using UnityEngine.InputSystem;

public class SplitAction : MonoBehaviour
{
    PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    public void SplitToggle(InputAction.CallbackContext context)
    {
        playerManager.SplitAction();
    }
}
