using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    float transitionTime = 1.0f;

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(SceneChange(sceneName));
    }
    IEnumerator SceneChange(string sceneName)
    {
        yield return new WaitForSeconds(transitionTime);
        AudioListener.pause = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
