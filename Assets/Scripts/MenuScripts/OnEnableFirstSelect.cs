using UnityEngine;
using UnityEngine.EventSystems;

public class OnEnableFirstSelect : MonoBehaviour
{
    [SerializeField]
    GameObject objectToSelect;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(objectToSelect);
    }
}
