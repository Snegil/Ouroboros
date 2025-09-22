using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedOnStart : MonoBehaviour
{
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);       
    }
}
