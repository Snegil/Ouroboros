using UnityEngine;
using UnityEngine.Video;

public class EnableObjectOnVideoEnd : MonoBehaviour
{
    [SerializeField]
    GameObject objectToEnable;

    VideoPlayer videoPlayer;

    float frameCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        frameCount = videoPlayer.frameCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.frame >= frameCount - 1)
        {
            objectToEnable.SetActive(true);
            enabled = false;
        }
    }
    public void SkipVideo()
    {
        videoPlayer.frame = (long)frameCount - 2;
    }
}
