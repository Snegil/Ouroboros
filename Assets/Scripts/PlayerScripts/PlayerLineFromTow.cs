using UnityEngine;

public class PlayerLineFromTow : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField]
    Transform playertailpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, playertailpoint.position);
    }
}
