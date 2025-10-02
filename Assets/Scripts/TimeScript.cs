using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    float time = 0f;

    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        text.text = time.ToString();
    }
}
