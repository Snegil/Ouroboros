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
        transform.position = lineRenderer.GetPosition(positionIndex);
    }
}
