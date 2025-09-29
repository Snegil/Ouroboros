using UnityEngine;

public class PlayerStunned : MonoBehaviour
{
    [SerializeField]
    float stunTimer = 0.5f;
    float setStunTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setStunTimer = stunTimer;
        stunTimer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.fixedDeltaTime;
            return;
        }
    }
    public void ActivateStunTimer()
    {
        stunTimer = setStunTimer;
    }
    public bool IsStunned()
    {
        return stunTimer != 0;
    }
}
