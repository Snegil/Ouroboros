using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPrefAndChangeSprite : MonoBehaviour
{
    [SerializeField]
    Image textSpriteRenderer;
    [SerializeField]
    Sprite victoryTextSprite;
    [Space, SerializeField]
    Image imageSpriteRenderer; 
    [SerializeField]
    Sprite victoryImageSprite;
    
    

    void Awake()
    {
        if (PlayerPrefs.HasKey("Victory"))
        {
            textSpriteRenderer.sprite = victoryTextSprite;
            imageSpriteRenderer.sprite = victoryImageSprite;

            PlayerPrefs.DeleteKey("Victory");
        }
    }
}