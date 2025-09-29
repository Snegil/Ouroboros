using UnityEngine;

public class BloodSystem : MonoBehaviour
{
    Transform objectToMoveTo;
    public Transform ObjectToMoveTo { set { objectToMoveTo = value; } }

    GameObject bloodParticleSystem;

    void Start()
    {
        bloodParticleSystem = transform.GetChild(0).gameObject;
        DisableBlood();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = objectToMoveTo.position;
    }
    public void EnableBlood()
    {
        bloodParticleSystem.SetActive(true);
    }
    public void DisableBlood()
    { 
        bloodParticleSystem.SetActive(false);
    }
}
