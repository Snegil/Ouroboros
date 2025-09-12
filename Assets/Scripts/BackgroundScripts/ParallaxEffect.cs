using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    float length;
    float startpos;
    GameObject cam;
    [SerializeField, Range(0f, 1f)]
    float parallaxEffect;

    void Start()
    {
        cam = Camera.main.gameObject;
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
