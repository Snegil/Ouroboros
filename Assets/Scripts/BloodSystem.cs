using UnityEngine;

public class BloodSystem : MonoBehaviour
{
    [SerializeField, Header("THE TOW BAR OF THE PLAYER THIS SHOULD FOLLOW!")]
    Transform objectToMoveTo;

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
