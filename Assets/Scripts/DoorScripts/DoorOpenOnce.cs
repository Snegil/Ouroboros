using UnityEngine;

public class DoorOpenOnce : MonoBehaviour
{
    [SerializeField]
    Vector2 openLocation;

    [Space, SerializeField, Header("The speed of the door opening.")]
    float lerpSpeed = 1f;

    float t = 0;

    [Space, SerializeField, Header("This is only used for the tolerance of\nhow close the door will have to be to the target location to count as 'reached'")]
    float distanceTolerance = 0.1f;

    void Start()
    {
        openLocation += (Vector2)transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, openLocation) < distanceTolerance)
        {
            enabled = false;
            return;
        }

        t += Time.deltaTime * lerpSpeed;

        transform.position = Vector2.MoveTowards(transform.position, openLocation, t);
    }
}
