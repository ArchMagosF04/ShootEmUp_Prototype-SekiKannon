using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 Rotate(this Vector2 originalVector, float rotateAngleInDegrees) //Rotates a vector in a given amount of degrees.
    {
        Quaternion rotation = Quaternion.AngleAxis(rotateAngleInDegrees, Vector3.forward);
        return rotation * originalVector;
    }
}
