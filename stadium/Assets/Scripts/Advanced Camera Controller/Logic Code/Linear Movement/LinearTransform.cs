using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LinearTransform : ICameraMovement
{
    public CameraTransformType transformType { get; } = CameraTransformType.Linear;
    public bool smoothnessEnabled { get; set; } = true;

    public Vector3? startPosition { get; set; }
    public Vector3 endPosition { get; set; }
    public Vector3? startRotation { get; set; }
    public Vector3 endRotation { get; set; }
    public float duration { get; set; }

    public LinearTransform() {}

    public LinearTransform(Vector3? startPosition, Vector3 endPosition, Vector3? startRotation, Vector3 endRotation, float duration)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.duration = duration;
    }

    public LinearTransform(Vector3? startPosition, Vector3 endPosition, Vector3? startRotation, Vector3 endRotation, float duration,
        bool smoothnessEnabled)
    {
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

        serializableLinearTransform.transformType = transformType.ToString();
        serializableLinearTransform.smoothnessEnabled = smoothnessEnabled;
        serializableLinearTransform.startPosition = SerializeHelper.Vector3ToList(startPosition);
        serializableLinearTransform.endPosition = SerializeHelper.Vector3ToList(endPosition);
        serializableLinearTransform.startRotation = SerializeHelper.Vector3ToList(startRotation);
        serializableLinearTransform.endRotation = SerializeHelper.Vector3ToList(endRotation);
        serializableLinearTransform.duration = duration;


        return JsonUtility.ToJson(serializableLinearTransform, prettyPrint);
    }

    public static LinearTransform FromJson(string json)
    {
        SerializableLinearTransform serializableLinearTransform = JsonUtility.FromJson<SerializableLinearTransform>(json);

        Vector3? startPosition = SerializeHelper.ListToNullableVector(serializableLinearTransform.startPosition);
        Vector3 endPosition = SerializeHelper.ListToVector(serializableLinearTransform.endPosition);
        Vector3? startRotation = SerializeHelper.ListToNullableVector(serializableLinearTransform.startRotation);
        Vector3 endRotation = SerializeHelper.ListToVector(serializableLinearTransform.endRotation);

        return new LinearTransform(startPosition, endPosition, startRotation, endRotation, serializableLinearTransform.duration,
                                   serializableLinearTransform.smoothnessEnabled);
    }
}

[Serializable]
public class SerializableLinearTransform
{
    public string transformType;
    public bool smoothnessEnabled;

    public List<float> startPosition;
    public List<float> endPosition;
    public List<float> startRotation;
    public List<float> endRotation;
    public float duration;
}


[Serializable]
public class LinearTransformParseError : Exception {
    public LinearTransformParseError() { }

    public LinearTransformParseError(string message) : base(message) { }
}