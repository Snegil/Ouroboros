using UnityEngine;
using UnityEngine.EventSystems;

public class OnEnableFirstSelect : MonoBehaviour
{
    [SerializeField]
    GameObject objectToSelect;
    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(objectToSelect);
    }
}
