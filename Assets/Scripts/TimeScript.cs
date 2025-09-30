using System.Threading;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    bool startcounting = false;
    float time = 0f;

    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        startcounting = true;    
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        text.text = time.ToString();
        Debug.Log(time);
    }
}
