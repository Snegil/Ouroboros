using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    [SerializeField, Header("The position the door will move to when opened.\nLOCAL POSITION")]
    Vector2 openLocation;
    Vector2 closeLocation;

    [Space, SerializeField, Header("The speed of the door opening and closing.")]
    float lerpSpeed = 1f;

    float t = 0;

    [Space, SerializeField, Header("This is only used for the tolerance of\nhow close the door will have to be to the target location to count as 'reached'")]
    float distanceTolerance = 0.1f;

    bool isOpen = false;

    bool doOnce = false;

    bool isMoving = false;
    public bool IsMoving() { return isMoving; }
    
    void Start()
    {
        if (doOnce) { return; }
        openLocation += (Vector2)transform.position;
        closeLocation += (Vector2)transform.position;
        doOnce = true;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, isOpen ? closeLocation : openLocation) < distanceTolerance)
        {
            isOpen = !isOpen;
            t = 0;
            isMoving = false;
            enabled = false;            
            return;
        }

        t += Time.deltaTime * lerpSpeed;
        isMoving = true;
        transform.position = Vector2.MoveTowards(transform.position, isOpen ? closeLocation : openLocation, t);
    }

}
