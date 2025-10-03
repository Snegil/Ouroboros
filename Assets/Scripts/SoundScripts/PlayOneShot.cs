using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayOneShot : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OneShotAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
