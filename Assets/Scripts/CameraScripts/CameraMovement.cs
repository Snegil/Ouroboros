using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerManager.AveragePosition().x, playerManager.AveragePosition().y, transform.position.z);
    }
}
