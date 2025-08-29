using UnityEngine;

public class PlayerManagerPosition : MonoBehaviour
{
    Transform thatSkink;
    Transform otherSkink;
    // Start is called before the first frame update

    public void SetPlayerTransforms(Transform thatSkink, Transform otherSkink)
    {
        this.thatSkink = thatSkink;
        this.otherSkink = otherSkink;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = (thatSkink.position + otherSkink.position) / 2f;
    }
}
