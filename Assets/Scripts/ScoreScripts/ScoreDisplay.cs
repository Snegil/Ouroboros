using UnityEngine;

[RequireComponent(typeof(ScoreManager))]
public class ScoreDisplay : MonoBehaviour
{
    ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
    }

    
}
