using UnityEngine;

public class Gravity : MonoBehaviour
{
    Vector2 gravity = new Vector2(-9.81f, -9.81f);

    public Vector2 GetYGravity()
    {
        return new Vector2(0, gravity.y);
    }
    public Vector2 GetXGravity()
    {
        return new Vector2(gravity.x, 0);
    }
}
