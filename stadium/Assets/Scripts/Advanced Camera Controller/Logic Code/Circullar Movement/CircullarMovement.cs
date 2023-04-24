using System;
using System.Collections.Generic;
using UnityEngine;

public class CircullarMovement: ICameraMovement
{
    public string movementName { get; set; }
    public CameraTransformType transformType { get; } = CameraTransformType.Circullar;
    public bool smoothnessEnabled { get; set; } = true;

    public Vector3? targetPosition { get; set; }
    public Vector3 startRotation { get; set; }
    public Vector3 endRotation { get; set; }
    public float startDistanceFromTarget { get; set; }
    public float endDistanceFromTarget { get; set; }
    public float duration { get; set; }

    public CircullarMovement(Vector3? targetPosition, Vector3 startRotation, Vector3 endRotation,
                              float startDistanceFromTarget, float endDistanceFromTarget, float duration)
    {
        movementName = "New Circullar Movement";
        this.targetPosition = targetPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.startDistanceFromTarget = startDistanceFromTarget;
        this.endDistanceFromTarget = endDistanceFromTarget;
        this.duration = duration;
    }

    public CircullarMovement(string movementName, Vector3? targetPosition, Vector3 startRotation, Vector3 endRotation,
                              float startDistanceFromTarget, float endDistanceFromTarget, float duration,
                              bool smoothnessEnabled)
    {
        this.movementName = movementName;
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

        serializableCircullarTransform.movementName = movementName;
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

    public static CircullarMovement FromJson(string json)
    {
        SerializableCircullarTransform serializableCircullarTransform = JsonUtility.FromJson<SerializableCircullarTransform>(json);

        string movementName = serializableCircullarTransform.movementName;
        Vector3? targetPosition = SerializeHelper.ListToNullableVector(serializableCircullarTransform.targetPosition);

        float startDistanceFromTarget = serializableCircullarTransform.startDistanceFromTarget;
        float endDistanceFromTarget = serializableCircullarTransform.endDistanceFromTarget;

        Vector3 startRotation = SerializeHelper.ListToVector(serializableCircullarTransform.startRotation);
        Vector3 endRotation = SerializeHelper.ListToVector(serializableCircullarTransform.endRotation);

        return new CircullarMovement(movementName, targetPosition, startRotation, endRotation, startDistanceFromTarget, endDistanceFromTarget,
                                      serializableCircullarTransform.duration, serializableCircullarTransform.smoothnessEnabled);
    }
}


[Serializable]
public class SerializableCircullarTransform
{
    public string movementName;
    public string transformType;
    public bool smoothnessEnabled;

    public List<float> targetPosition;
    public List<float> startRotation;
    public List<float> endRotation;
    public float startDistanceFromTarget;
    public float endDistanceFromTarget;
    public float duration;
}