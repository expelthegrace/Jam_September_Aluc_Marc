using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions 
{
    public static Vector2 RotateVector2(this Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }
}
