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

    public static Vector3 GetReflectionVector(Vector3 aVector, Vector3 aNormal)
    {
        return aVector - 2 * (Vector3.Dot(aVector, aNormal)) * aNormal;
    }

    public static bool AreAlmostSameVector(Vector3 aVector1, Vector3 aVector2, float aMarginAngle = 0.0f)
    {
        return Vector3.Angle(aVector1.normalized, aVector2.normalized) <= aMarginAngle;
    }
    
    public static Vector3 Rotate2DVector3(Vector3 aVector, float aAngle)
    {
        return Quaternion.AngleAxis(aAngle, -Vector3.forward) * aVector;
    }
}
