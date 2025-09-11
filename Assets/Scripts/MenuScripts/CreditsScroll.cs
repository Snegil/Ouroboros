using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    Vector3 originalPosition;
    [SerializeField]
    float targetYPosition;
    [SerializeField]
    float scrollSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = GetComponent<RectTransform>().position;
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().position += scrollSpeed * Time.deltaTime * Vector3.up;

        if (GetComponent<RectTransform>().position.y >= targetYPosition)
        {
            GetComponent<RectTransform>().position = originalPosition;
            //enabled = false;
        }
    }
}
