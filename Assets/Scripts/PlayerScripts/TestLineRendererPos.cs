using UnityEngine;

public class TestLineRendererPos : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer;

    [SerializeField]
    int positionIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer.positionCount == 0 || lineRenderer.GetPosition(positionIndex) == null) { return; }

        transform.position = lineRenderer.GetPosition(positionIndex);
    }
}
