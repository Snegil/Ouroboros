using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    void Start()
    {
        if (audioSource != null) return;

        audioSource = GetComponent<AudioSource>();
    }
    public void OneShotAudio(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }
}
