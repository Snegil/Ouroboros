using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    float score = 0;

    [SerializeField]
    float multiplier = 1;

    PlayerManager playerManager;

    [SerializeField]
    bool displayDebugLog = false;

    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();    
    }
    // Update is called once per frame
    void Update()
    {
        score = Mathf.Clamp(playerManager.AveragePosition().y * multiplier, 0, Mathf.Infinity);
        if (displayDebugLog) { Debug.Log("Score: " + score); }        
    }
}
