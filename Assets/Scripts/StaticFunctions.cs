using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions 
{
    public static Vector2 RotateVector2(this Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }

    public static Quaternion Get2DQuaternionFromDirection(Vector3 aDirection)
    {
        var angle = Mathf.Atan2(aDirection.y, aDirection.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
