using UnityEngine;

public class CircularSaw : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 100f;
    [SerializeField]
    float explosiveForce = 10f;
    [SerializeField]
    float upwardForce = 2f;

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerManager")) return;

        collision.GetComponent<PlayerManager>().HazardSplit(transform.position, explosiveForce, upwardForce);
    }
}
