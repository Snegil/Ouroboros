using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public void Toggle(GameObject toToggle)
    {
        if (toToggle == null) return;
        toToggle.SetActive(!toToggle.activeSelf);
    }
}
