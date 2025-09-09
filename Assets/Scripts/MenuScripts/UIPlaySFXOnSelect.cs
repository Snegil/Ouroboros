using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlaySFXOnSelect : MonoBehaviour
{
    [SerializeField]
    AudioSource source;

    public void PlayOnSelect()
    {
        source.PlayOneShot(source.clip);    
    }
}
