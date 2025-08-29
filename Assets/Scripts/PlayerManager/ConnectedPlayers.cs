using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(LineRenderer))]

// This script is a test for skink project.


public class ConnectedPlayers : MonoBehaviour
{
    PlayerManager playerManager;

    LineRenderer lineRenderer;
    PolygonCollider2D meshCollider;

    Transform thatSkink;
    Transform otherSkink;

    [SerializeField]
    int segments = 10;
    [SerializeField]
    float sagAmount = 1f;

    Mesh lineMesh;
    Camera mainCamera;
    void Start()
    {
        lineMesh = new Mesh();
        mainCamera = Camera.main;
        playerManager = GetComponent<PlayerManager>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        lineRenderer.useWorldSpace = true;
    }
    void Update()
    {
        if (!playerManager.IsJoint) { return; }

        Vector3 thatSkink = playerManager.GetThatSkink().transform.position;
        Vector3 otherSkink = playerManager.GetOtherSkink().transform.position;

        Vector3 b = (thatSkink + otherSkink) / 2f;
        b.y -= sagAmount;
        b.z = 0f;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, thatSkink, b, otherSkink);
            point.z = 0f;

            lineRenderer.SetPosition(i, point);
        }
        
    }
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 a, Vector3 b, Vector3 c)
    {
        float u = 1 - t;
        return u * u * a + 2 * u * t * b + t * t * c;
    }
    public void ToggleLine()
    {
        lineRenderer.positionCount = lineRenderer.positionCount == 0 ? segments : 0;
    }
}

//(transform.position.x + otherSkink.position.x) / 2f
//transform.position.y + otherSkink.position.y + connectedHeight

/*
[SerializeField]
    Transform otherSkink;

    LineRenderer lineRenderer;

    [SerializeField]
    float xConnectedOffset = 2f;
    [SerializeField]
    float yConnectedOffset = 2f;
    [SerializeField]
    float tolerance = 0.1f;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.positionCount = 3;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new(
                                 Mathf.Abs(transform.position.x - otherSkink.position.x) > tolerance ? (transform.position.x + otherSkink.position.x) / 2 : xConnectedOffset,
                                 Mathf.Abs(transform.position.y - otherSkink.position.y) > tolerance ? (transform.position.y + otherSkink.position.y) / 2 : yConnectedOffset,
                                 0));
        lineRenderer.SetPosition(2, otherSkink.position);
    }
*/