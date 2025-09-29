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
        else
        {
            transform.position = new Vector2(1000f, 1000f);
        }
        
    }
    public void EnableBlood()
    {
        bloodEnabled = true;
    }
    public void DisableBlood()
    { 
        bloodEnabled = false;
    }
}
