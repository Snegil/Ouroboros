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
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(transitionTime);
        AudioListener.pause = false;        
        Debug.Log("Changing Scene to " + sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);        
    }
}
