using System.Collections;
using UnityEngine;

public class DoorTimed : MonoBehaviour
{
    [SerializeField, Header("The position the door will move to when opened.\nLOCAL POSITION")]
    Vector2 openLocation;
    Vector2 closeLocation;

    [Space, SerializeField, Header("The speed of the door opening and closing.")]
    float lerpSpeed = 1f;

    float t = 0;

    [Space, SerializeField, Header("This is only used for the tolerance of\nhow close the door will have to be to the target location to count as 'reached'")]
    float distanceTolerance = 0.1f;

    [SerializeField, Header("The time the door will stay open before closing.")]
    float timeDoorStaysOpen = 2f;
    float setTimeDoorStaysOpen;

    bool isOpen = false;
    bool doOnce = false;

    bool isMoving = false;
    public bool IsMoving() { return isMoving; }

    bool coroutineRunning = false;

    void Start()
    {
        if (doOnce) { return; }
        setTimeDoorStaysOpen = timeDoorStaysOpen;
        openLocation += (Vector2)transform.position;
        closeLocation += (Vector2)transform.position;
        doOnce = true;
    }

    void Update()
    {
        if (coroutineRunning) return; 
        
        if (!isOpen)
        {
            t += lerpSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, openLocation, t);
            if (Vector2.Distance(transform.position, openLocation) < distanceTolerance)
            {
                isOpen = true;
            }
            return;
        }

        timeDoorStaysOpen -= Time.deltaTime;
        if (timeDoorStaysOpen > 0) { return; }

        if (isOpen)
        {
            t += lerpSpeed * Time.deltaTime;
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, closeLocation, t);
            if (Vector2.Distance(transform.position, closeLocation) < distanceTolerance)
            {
                isOpen = false;
                timeDoorStaysOpen = setTimeDoorStaysOpen;
                
                StartCoroutine(DisableIsMoving());
            }
            return;
        }
    }
    IEnumerator DisableIsMoving()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        isMoving = false;
        coroutineRunning = false;
        enabled = false;
    }
}
