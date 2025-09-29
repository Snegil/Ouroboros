using UnityEngine;

public class BloodSystem : MonoBehaviour
{
    Transform objectToMoveTo;
    public Transform ObjectToMoveTo { set { objectToMoveTo = value; } }

    GameObject bloodParticleSystem;

    bool bloodEnabled = false;

    void Start()
    {
        bloodParticleSystem = transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (bloodEnabled)
        {
            transform.position = objectToMoveTo.position;
        }        
    }
    public void EnableBlood()
    {
        bloodParticleSystem.SetActive(true);
        bloodEnabled = true;
    }
    public void DisableBlood()
    { 
        bloodParticleSystem.SetActive(false);
        bloodEnabled = false;
    }
}
