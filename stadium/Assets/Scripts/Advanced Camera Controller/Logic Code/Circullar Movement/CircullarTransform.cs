using System;
using System.Collections.Generic;
using UnityEngine;

public class CircullarTransform: ICameraMovement
{
    public CameraTransformType transformType { get; } = CameraTransformType.Circullar;
    public bool smoothnessEnabled { get; set; } = true;

    public Vector3? targetPosition { get; set; }
    public Vector3 startRotation { get; set; }
    public Vector3 endRotation { get; set; }
    public float startDistanceFromTarget { get; set; }
    public float endDistanceFromTarget { get; set; }
    public float duration { get; set; }

    public CircullarTransform(Vector3? targetPosition, Vector3 startRotation, Vector3 endRotation,
                              float startDistanceFromTarget, float endDistanceFromTarget, float duration)
    {
        this.targetPosition = targetPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.startDistanceFromTarget = startDistanceFromTarget;
        this.endDistanceFromTarget = endDistanceFromTarget;
        this.duration = duration;
    }

    public CircullarTransform(Vector3? targetPosition, Vector3 startRotation, Vector3 endRotation,
                              float startDistanceFromTarget, float endDistanceFromTarget, float duration,
                              bool smoothnessEnabled)
    {
        this.targetPosition = targetPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.startDistanceFromTarget = startDistanceFromTarget;
        this.endDistanceFromTarget = endDistanceFromTarget;
        this.duration = duration;
        this.smoothnessEnabled = smoothnessEnabled;
    }

    public string ToJson(bool prettyPrint)
    {
        SerializableCircullarTransform serializableCircullarTransform = new SerializableCircullarTransform();

        serializableCircullarTransform.transformType = transformType.ToString();
        serializableCircullarTransform.smoothnessEnabled = smoothnessEnabled;
        serializableCircullarTransform.targetPosition = SerializeHelper.Vector3ToList(targetPosition);
        serializableCircullarTransform.startDistanceFromTarget = startDistanceFromTarget;
        serializableCircullarTransform.endDistanceFromTarget = endDistanceFromTarget;
        serializableCircullarTransform.startRotation = SerializeHelper.Vector3ToList(startRotation);
        serializableCircullarTransform.endRotation = SerializeHelper.Vector3ToList(endRotation);
        serializableCircullarTransform.duration = duration;
        

        return JsonUtility.ToJson(serializableCircullarTransform, prettyPrint);
    }

    public static CircullarTransform FromJson(string json)
    {
        SerializableCircullarTransform serializableCircullarTransform = JsonUtility.FromJson<SerializableCircullarTransform>(json);

        Vector3? targetPosition = SerializeHelper.ListToNullableVector(serializableCircullarTransform.targetPosition);

        float startDistanceFromTarget = serializableCircullarTransform.startDistanceFromTarget;
        float endDistanceFromTarget = serializableCircullarTransform.endDistanceFromTarget;

        Vector3 startRotation = SerializeHelper.ListToVector(serializableCircullarTransform.startRotation);
        Vector3 endRotation = SerializeHelper.ListToVector(serializableCircullarTransform.endRotation);

        return new CircullarTransform(targetPosition, startRotation, endRotation, startDistanceFromTarget, endDistanceFromTarget,
                                      serializableCircullarTransform.duration, serializableCircullarTransform.smoothnessEnabled);
    }
}


[Serializable]
public class SerializableCircullarTransform : Exception
{
    public string transformType { get; set; }
    public bool smoothnessEnabled { get; set; }

    public List<float> targetPosition { get; set; }
    public List<float> startRotation { get; set; }
    public List<float> endRotation { get; set; }
    public float startDistanceFromTarget { get; set; }
    public float endDistanceFromTarget { get; set; }
    public float duration { get; set; }
}

[Serializable]
public class CircullarTransformParseError : Exception
{
    public CircullarTransformParseError() { }

    public CircullarTransformParseError(string message) : base(message) { }
}