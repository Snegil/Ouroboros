using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    AudioSource audioSource;

    public void OneShotAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
