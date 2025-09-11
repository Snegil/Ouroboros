using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerPrefabManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerOne;
    [SerializeField] private PlayerInput playerTwo;


    private void Awake()
    {
        SetControllersToPlayers();
    }
    public void SetControllersToPlayers()
    {
        var gamepads = Gamepad.all;

        if (gamepads.Count > 0)
        {
            playerOne.user.UnpairDevices(); // clear old
            InputUser.PerformPairingWithDevice(gamepads[0], playerOne.user);
            playerOne.SwitchCurrentControlScheme(gamepads[0]);
        }

        if (gamepads.Count > 1)
        {
            playerTwo.user.UnpairDevices();
            InputUser.PerformPairingWithDevice(gamepads[1], playerTwo.user);
            playerTwo.SwitchCurrentControlScheme(gamepads[1]);
        }
    }
}
