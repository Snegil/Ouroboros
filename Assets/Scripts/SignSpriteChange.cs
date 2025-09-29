using System.Collections.Generic;
using UnityEngine;

public class SignSpriteChange : MonoBehaviour
{
    [SerializeField, Header("The time between sprite changes")]
    float timer;
    float setTimer;

    [SerializeField, Header("The sprites to change between")]
    List<Sprite> sprites = new List<Sprite>();

    int index = 0;

    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setTimer = timer;
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = setTimer;
            if (sprites.Count == 0) return;
            spriteRenderer.sprite = sprites[index];
            index++;
            if (index >= sprites.Count) index = 0;
        }
    }
}
