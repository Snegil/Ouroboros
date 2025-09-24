using UnityEngine;

public class CircularSaw : MonoBehaviour
{
    [SerializeField]
    float explosiveForce = 10f;
    [SerializeField]
    float upwardForce = 2f;

    PlayerManager playerManager;
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerManager.HazardSplit(transform, collision.gameObject, explosiveForce, upwardForce);
    }
}
