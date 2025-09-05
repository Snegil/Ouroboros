using UnityEngine;

public class PlayerLineManager : MonoBehaviour
{
    PlayerManager playerManager;


    GameObject playerOne;
    GameObject playerOneAttachment;
    LineRenderer playerOneLineRenderer;

    GameObject playerTwo;
    GameObject playerTwoAttachment;
    LineRenderer playerTwoLineRenderer;

    [SerializeField]
    int segments = 3;
    [SerializeField]
    float sagAmount = 1f;

    [SerializeField]
    float sagTolerance = 0.1f;
    [SerializeField]
    float sagDivisor = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mathf.Clamp(segments, 3, int.MaxValue);


        playerManager = gameObject.GetComponent<    PlayerManager>();

        playerOne = playerManager.GetPlayerOne();
        playerTwo = playerManager.GetPlayerTwo();
        playerOneAttachment = playerOne.transform.GetChild(0).gameObject;
        playerTwoAttachment = playerTwo.transform.GetChild(0).gameObject;

        playerOneLineRenderer = playerOne.GetComponentInChildren<LineRenderer>();
        playerTwoLineRenderer = playerTwo.GetComponentInChildren<LineRenderer>();
        playerOneLineRenderer.positionCount = segments;
        playerTwoLineRenderer.positionCount = segments;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerManager.IsJoint) return;

        sagAmount = playerManager.DistanceBetweenCentreAndPlayerOne() <= sagTolerance ? 0f : (playerManager.DistanceBetweenCentreAndPlayerOne() - 1f) / sagDivisor;

        Vector3 playerOnePosition = playerOne.transform.position;
        Vector3 b1 = (playerOnePosition + transform.position) / 2f;
        b1.y -= sagAmount;
        b1.z = 0f;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, playerOnePosition, b1, transform.position);
            point.z = 0f;
            playerOneLineRenderer.SetPosition(i, point);
        }

        sagAmount = playerManager.DistanceBetweenCentreAndPlayerTwo() <= sagTolerance ? 0f : (playerManager.DistanceBetweenCentreAndPlayerTwo() - 1f) / sagDivisor;

        Vector3 playerTwoPosition = playerTwo.transform.position;
        Vector3 b2 = (playerTwoPosition + transform.position) / 2f;
        b2.y -= sagAmount;
        b2.z = 0f;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, playerTwoPosition, b2, transform.position);
            point.z = 0f;
            playerTwoLineRenderer.SetPosition(i, point);
        }

    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 a, Vector3 b, Vector3 c)
    {
        float u = 1 - t;
        return u * u * a + 2 * u * t * b + t * t * c;
    }

    public void ToggleLines()
    {
        playerOneLineRenderer.positionCount = playerOneLineRenderer.positionCount == 0 ? segments : 0;
        playerTwoLineRenderer.positionCount = playerTwoLineRenderer.positionCount == 0 ? segments : 0;
    }
}
        
        // if (!isConnected) return;

        // playerOneLineRenderer.SetPosition(0, playerOne.transform.position);
        // playerOneLineRenderer.SetPosition(1, playerOneAttachment.transform.position);
        // playerOneLineRenderer.SetPosition(2, transform.position);
        // playerTwoLineRenderer.SetPosition(0, playerTwo.transform.position);
        // playerTwoLineRenderer.SetPosition(1, playerTwoAttachment.transform.position);
        // playerTwoLineRenderer.SetPosition(2, transform.position);