using UnityEngine;

public class PlayerBlinkingScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField, Header("Blink Timer\nX= Min, Y = Max")]
    Vector2 timerRange;
    [SerializeField]
    float blinkTimer;

    // Update is called once per frame
    void Update()
    {
        blinkTimer -= Time.deltaTime;

        if (blinkTimer > 0) return;

        animator.SetTrigger("Blink");

        blinkTimer = Random.Range(timerRange.x, timerRange.y);
    }
}
