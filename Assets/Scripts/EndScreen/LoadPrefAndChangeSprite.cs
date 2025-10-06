using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPrefAndChangeSprite : MonoBehaviour
{
    [SerializeField]
    GameObject loseObject;
    [SerializeField]
    GameObject winObject;

    
    

    void Awake()
    {
        if (PlayerPrefs.HasKey("Victory"))
        {
            Debug.LogWarning("HAS PREF VICTORY");
            loseObject.SetActive(false);
            winObject.SetActive(true);

            PlayerPrefs.DeleteKey("Victory");
        }
    }
}