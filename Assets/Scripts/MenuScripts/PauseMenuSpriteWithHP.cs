using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuSpriteWithHP : MonoBehaviour
{
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    PlayerHPManager playerHPManager;

    Image image;

    private void Awake()
    {
        playerHPManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerHPManager>();
        image = GetComponent<Image>();
    }
    void OnEnable()
    {        
        if (playerHPManager.HP > 66)
        {
            image.sprite = sprites[0];
        }
        else if (playerHPManager.HP > 33)
        {
            image.sprite = sprites[1];
        }
        else
        {
            image.sprite = sprites[2];
        }   
    }
}
