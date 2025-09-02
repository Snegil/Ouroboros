using UnityEngine;

public class PlayerManagerPosition : MonoBehaviour
{
    PlayerManager playerManager;
    public void SetPlayerManager(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = playerManager.AveragePosition();
    }
}
