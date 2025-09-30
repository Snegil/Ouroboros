using UnityEngine;

public class ChangeSpriteOnPress : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite spriteToChangeTo;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerOne") || collision.gameObject.CompareTag("PlayerTwo") || collision.gameObject.CompareTag("PropTrigger"))
        {
            if (spriteRenderer == null || spriteToChangeTo == null)
            {
                Debug.LogWarning(gameObject.name + " IS MISSING A REFERENCE");
                return;
            }
            spriteRenderer.sprite = spriteToChangeTo;
            
            enabled = false; // Disable this script after changing the sprite
        }
    }
}
