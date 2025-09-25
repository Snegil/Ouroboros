using UnityEngine;

public class PlayerBloodCreator : MonoBehaviour
{
    [SerializeField]
    GameObject bloodSystemPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject instantiated = Instantiate(bloodSystemPrefab, transform);
        instantiated.GetComponent<BloodSystem>().ObjectToMoveTo = transform.GetChild(0).transform;
    }
}
