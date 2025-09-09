using UnityEngine;

public class TowLookAtPlayer : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(transform.parent);
    }
}
