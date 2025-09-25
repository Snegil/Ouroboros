using UnityEngine;

public class BloodSystem : MonoBehaviour
{
    Transform objectToMoveTo;
    public Transform ObjectToMoveTo { set { objectToMoveTo = value; } }

    ParticleSystem bloodParticleSystem;
    ParticleSystem.EmissionModule emissionModule;

    float originalRateOverTime;
    float originalRateOverDistance;

    void Start()
    {
        bloodParticleSystem = GetComponent<ParticleSystem>();
        emissionModule = bloodParticleSystem.emission;
        originalRateOverDistance = emissionModule.rateOverDistance.constant;
        originalRateOverTime = emissionModule.rateOverTime.constant;

        DisableBlood();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = objectToMoveTo.position;
    }
    public void EnableBlood()
    {
        emissionModule.rateOverTime = originalRateOverTime;
        emissionModule.rateOverDistance = originalRateOverDistance;        
    }
    public void DisableBlood()
    {
        emissionModule.rateOverTime = 0;
        emissionModule.rateOverDistance = 0;
    }
}
