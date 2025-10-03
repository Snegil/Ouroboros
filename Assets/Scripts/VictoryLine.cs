using UnityEngine;

public class VictoryLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerOne") || collision.CompareTag("PlayerTwo"))
        {
            PlayerPrefs.SetInt("Victory", 1);
            GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<SceneTransition>().ChangeScene("EndScreen");
        }
    }
}
