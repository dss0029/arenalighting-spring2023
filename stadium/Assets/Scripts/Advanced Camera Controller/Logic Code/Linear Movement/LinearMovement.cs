using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LinearMovement : ICameraMovement
{
    public string movementName { get; set; }
    public CameraTransformType transformType { get; } = CameraTransformType.Linear;
    public bool smoothnessEnabled { get; set; } = true;

    public Vector3? startPosition { get; set; }
    public Vector3 endPosition { get; set; }
    public Vector3? startRotation { get; set; }
    public Vector3 endRotation { get; set; }
    public float duration { get; set; }

    public LinearMovement() {}

    public LinearMovement(Vector3? startPosition, Vector3 endPosition, Vector3? startRotation, Vector3 endRotation, float duration)
    {
        movementName = "New Linear Movement";
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.duration = duration;
    }

    public LinearMovement(string movementName, Vector3? startPosition, Vector3 endPosition, Vector3? startRotation, Vector3 endRotation, float duration,
        bool smoothnessEnabled)
    {
        this.movementName = movementName;
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.duration = duration;
        this.smoothnessEnabled = smoothnessEnabled;
    }

    public string ToJson(bool prettyPrint)
    {
        SerializableLinearTransform serializableLinearTransform = new SerializableLinearTransform();

        serializableLinearTransform.movementName = movementName;
        serializableLinearTransform.transformType = transformType.ToString();
        serializableLinearTransform.smoothnessEnabled = smoothnessEnabled;
        serializableLinearTransform.startPosition = SerializeHelper.Vector3ToList(startPosition);
        serializableLinearTransform.endPosition = SerializeHelper.Vector3ToList(endPosition);
        serializableLinearTransform.startRotation = SerializeHelper.Vector3ToList(startRotation);
        serializableLinearTransform.endRotation = SerializeHelper.Vector3ToList(endRotation);
        serializableLinearTransform.duration = duration;


        return JsonUtility.ToJson(serializableLinearTransform, prettyPrint);
    }

    public static LinearMovement FromJson(string json)
    {
        SerializableLinearTransform serializableLinearTransform = JsonUtility.FromJson<SerializableLinearTransform>(json);

        string movementName = serializableLinearTransform.movementName;
        Vector3? startPosition = SerializeHelper.ListToNullableVector(serializableLinearTransform.startPosition);
        Vector3 endPosition = SerializeHelper.ListToVector(serializableLinearTransform.endPosition);
        Vector3? startRotation = SerializeHelper.ListToNullableVector(serializableLinearTransform.startRotation);
        Vector3 endRotation = SerializeHelper.ListToVector(serializableLinearTransform.endRotation);

        return new LinearMovement(movementName, startPosition, endPosition, startRotation, endRotation, serializableLinearTransform.duration,
                                   serializableLinearTransform.smoothnessEnabled);
    }
}

[Serializable]
public class SerializableLinearTransform
{
    public string movementName;
    public string transformType;
    public bool smoothnessEnabled;

    public List<float> startPosition;
    public List<float> endPosition;
    public List<float> startRotation;
    public List<float> endRotation;
    public float duration;
}