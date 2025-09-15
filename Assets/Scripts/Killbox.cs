using UnityEngine;

public class Killbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerOne") || collision.CompareTag("PlayerTwo"))
        {
            GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerHPManager>().PlayerDeath();
        }        
    }
}
