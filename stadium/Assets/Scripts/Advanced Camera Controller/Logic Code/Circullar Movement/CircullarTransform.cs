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

    public Dictionary<string, object> toDict()
    {
        List<float?> targetPositionList = new List<float?>() { null, null, null };

        if (targetPosition.HasValue)
        {
            targetPositionList = new List<float?>() { targetPosition.Value.x, targetPosition.Value.x, targetPosition.Value.z };
        }

        Dictionary<string, object> circullarTransformDictionary = new Dictionary<string, object> {
            { "transformType", transformType.ToString()},
            { "smoothnessEnabled", smoothnessEnabled },
            { "targetPosition", targetPositionList },
            { "startDistanceFromTarget", startDistanceFromTarget },
            { "endDistanceFromTarget", endDistanceFromTarget },
            { "startRotation", new List<float>() { startRotation.x, startRotation.y, startRotation.z } },
            { "endRotation", new List<float>() { endRotation.x, endRotation.y, endRotation.z } },
            { "duration", duration },
        };

        return circullarTransformDictionary;
    }

    public ICameraMovement fromDict(Dictionary<string, object> dict)
    {
        return staticFromDict(dict);
    }

    public static CircullarTransform staticFromDict(Dictionary<string, object> dict)
    {
        if ((string)dict["transformType"] == "Circullar")
        {
            throw new LinearTransformParseError("Expected transform type \"Linear\", was " + dict["transformType"]);
        }

        bool smoothnessEnabled = (bool)dict["smoothnessEnabled"];

        // Parse start position
        Vector3? targetPosition = null;
        List<float> targetPositionList = (List<float>)dict["targetPosition"];
        if (targetPositionList != null)
        {
            targetPosition = new Vector3(targetPositionList[0], targetPositionList[1], targetPositionList[2]);
        }

        // Parse distances from target
        float startDistanceFromTarget = (float)dict["startDistanceFromTarget"];
        float endDistanceFromTarget = (float)dict["endDistanceFromTarget"];

        // Parse start position
        List<float> startRotationList = (List<float>)dict["startRotation"];
        Vector3 startRotation = new Vector3(startRotationList[0], startRotationList[1], startRotationList[2]);

        // Parse end position
        List<float> endRotationList = (List<float>)dict["endRotation"];
        Vector3 endRotation = new Vector3(endRotationList[0], endRotationList[1], endRotationList[2]);

        // Parse duration
        float duration = (float)dict["duration"];

        return new CircullarTransform(targetPosition, startRotation, endRotation, startDistanceFromTarget, endDistanceFromTarget, duration,
                                      smoothnessEnabled);
    }
}


[Serializable]
public class CircullarTransformParseError : Exception
{
    public CircullarTransformParseError() { }

    public CircullarTransformParseError(string message) : base(message) { }
}