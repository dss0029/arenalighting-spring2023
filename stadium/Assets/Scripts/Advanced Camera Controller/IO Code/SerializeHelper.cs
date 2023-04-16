using System.Collections.Generic;
using UnityEngine;

public static class SerializeHelper
{
    public static List<float> Vector3ToList(Vector3? vector3)
    {
        if (vector3.HasValue)
        {
            return new List<float>() { vector3.Value.x, vector3.Value.y, vector3.Value.z };
        }

        return null;
    }

    public static Vector3 ListToVector(List<float> floats)
    {
        return new Vector3(floats[0], floats[1], floats[2]);
    }

    public static Vector3? ListToNullableVector(List<float> floats)
    {
        if (floats == null || floats.Count == 0)
        {
            return null;
        }
        return new Vector3(floats[0], floats[1], floats[2]);
    }

    public static float[] Vector3ToArray(Vector3? vector3)
    {
        if (vector3.HasValue)
        {
            return new float[3] { vector3.Value.x, vector3.Value.y, vector3.Value.z };
        }

        return null;
    }

    public static Vector3 ArrayToVector(float[] floats)
    {
        return new Vector3(floats[0], floats[1], floats[2]);
    }

    public static Vector3? ArrayToNullableVector(float[] floats)
    {
        if (floats == null)
        {
            return null;
        }

        return new Vector3(floats[0], floats[1], floats[2]);
    }
}
