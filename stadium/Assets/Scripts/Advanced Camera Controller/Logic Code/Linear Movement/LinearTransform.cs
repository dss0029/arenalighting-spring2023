using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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

    public Dictionary<string, object> toDict()
    {
        List<float> startPositionList = null;
        List<float> startRotationList = null;

        if (startPosition.HasValue) {
            startPositionList = new List<float>() { startPosition.Value.x, startPosition.Value.x, startPosition.Value.z };
        }

        if (startRotation.HasValue)
        {
            startRotationList = new List<float>() { startRotation.Value.x, startRotation.Value.x, startRotation.Value.z };
        }

        Dictionary<string, object> linearTransformDictionary = new Dictionary<string, object> {
            { "transformType", transformType.ToString()},
            { "smoothnessEnabled", smoothnessEnabled },
            { "startPosition", startPositionList },
            { "endPosition", new List<float>() { endPosition.x, endPosition.y, endPosition.z } },
            { "startRotation", startRotationList },
            { "endRotation", new List<float>() { endRotation.x, endRotation.y, endRotation.z } },
            { "duration", duration },
        };

        return linearTransformDictionary;
    }

    public ICameraMovement fromDict( Dictionary<string, object> dict )
    {
        return staticFromDict( dict );
    }

    public static LinearTransform staticFromDict( Dictionary<string, object> dict )
    {
        if ((string) dict["transformType"] == "Linear")
        {
            throw new LinearTransformParseError("Expected transform type \"Linear\", was " + dict["transformType"]);
        }

        bool smoothnessEnabled = (bool)dict["smoothnessEnabled"];

        Debug.Log("LinearTransform - Parsed smoothnessEnabled");

        // Parse start position
        Vector3? startPosition = null;
        List<float> startPositionList = (List<float>)dict["startPosition"];
        if (startPositionList != null)
        {
            startPosition = new Vector3(startPositionList[0], startPositionList[1], startPositionList[2]);
        }

        Debug.Log("LinearTransform - Parsed startPosition");

        // Parse end position
        List<float> endPositionList = (List<float>)dict["endPosition"];
        Vector3 endPosition = new Vector3(endPositionList[0], endPositionList[1], endPositionList[2]);

        Debug.Log("LinearTransform - Parsed endPosition");

        // Parse start position
        Vector3? startRotation = null;
        List<float> startRotationList = (List<float>)dict["startRotation"];
        if (startRotationList != null)
        {
            startRotation = new Vector3(startRotationList[0], startRotationList[1], startRotationList[2]);
        }

        Debug.Log("LinearTransform - Parsed startRotationList");

        // Parse end position
        List<float> endRotationList = (List<float>)dict["endRotation"];
        Vector3 endRotation = new Vector3(endRotationList[0], endRotationList[1], endRotationList[2]);

        Debug.Log("LinearTransform - Parsed endRotation");

        // Parse duration
        float duration = (float)dict["duration"];

        Debug.Log("LinearTransform - Parsed duration");

        return new LinearTransform(startPosition, endPosition, startRotation, endRotation, duration, smoothnessEnabled);
    }

    public SerializableLinearTransform Serialize()
    {
        List<float> endPositionList = new List<float>() { endPosition.x, endPosition.y, endPosition.z };
        List<float> endRotationList = new List<float>() { endRotation.x, endRotation.y, endRotation.z };

        List<float> startPositionList = null;
        List<float> startRotationList = null;

        if (startPosition.HasValue)
        {
            startPositionList = new List<float>() { startPosition.Value.x, startPosition.Value.x, startPosition.Value.y };
        }

        if (startRotation.HasValue)
        {
            startRotationList = new List<float>() { startRotation.Value.x, startRotation.Value.x, startRotation.Value.z };
        }

        SerializableLinearTransform serializableLinearTransform = new SerializableLinearTransform();
        serializableLinearTransform.transformType = transformType.ToString();
        serializableLinearTransform.smoothnessEnabled = smoothnessEnabled;
        serializableLinearTransform.startPosition = startPositionList;
        serializableLinearTransform.endPosition = endPositionList;
        serializableLinearTransform.startRotation = startRotationList;
        serializableLinearTransform.endRotation = endRotationList;
        serializableLinearTransform.duration = duration;

        return serializableLinearTransform;
    }

    public static LinearTransform Deserialize(SerializableLinearTransform serializableLinearTransform)
    {
        Vector3 endPosition = new Vector3();
        Vector3 endRotation = new Vector3();

        Vector3? startPosition = null;
        Vector3? startRotation = null;
        
        if (serializableLinearTransform.startPosition != null)
        {
            startPosition = new Vector3(serializableLinearTransform.startPosition[0],
                                        serializableLinearTransform.startPosition[1],
                                        serializableLinearTransform.startPosition[2]);
        }

        if (serializableLinearTransform.startRotation != null)
        {
            startRotation = new Vector3(serializableLinearTransform.startRotation[0],
                                        serializableLinearTransform.startRotation[1],
                                        serializableLinearTransform.startRotation[2]);
        }



        return new LinearTransform(startPosition, endPosition, startRotation, endRotation, serializableLinearTransform.duration,
                                   serializableLinearTransform.smoothnessEnabled);
    }

    private List<float> Vector3ToList(Vector3? vector3)
    {
        if (vector3.HasValue)
        {
            return new List<float>() { vector3.Value.x, vector3.Value.y, vector3.Value.z };
        }

        return null;
    }

    private Vector3 ListToVector(List<float> floats)
    {
        return new Vector3(floats[0], floats[1], floats[2]);
    }

    private Vector3? ListToNullableVector(List<float> floats)
    {
        if (floats == null)
        {
            return null;
        }

        return new Vector3(floats[0], floats[1], floats[2]);
    }

    // Implement this method to serialize data. The method is called
    // on serialization.
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {

        //CameraTransformType transformType { get; } = CameraTransformType.Linear;
        //bool smoothnessEnabled { get; set; } = true;

        //Vector3? startPosition { get; set; }
        //Vector3 endPosition { get; set; }
        //Vector3? startRotation { get; set; }
        //Vector3 endRotation { get; set; }
        //float duration { get; set; }
    
        // Use the AddValue method to specify serialized values.
        info.AddValue("transformType", transformType, typeof(CameraTransformType));
        info.AddValue("smoothnessEnabled", smoothnessEnabled, typeof(bool));
        info.AddValue("startPosition", Vector3ToList(startPosition), typeof(List<float>));
        info.AddValue("endPosition", endPosition, typeof(List<float>));
        info.AddValue("startRotation", startRotation, typeof(List<float>));
        info.AddValue("endRotation", endRotation, typeof(List<float>));
        info.AddValue("duration", duration, typeof(float));
    }

    // The special constructor is used to deserialize values.
    public LinearTransform(SerializationInfo info, StreamingContext context)
    {
        // Reset the property value using the GetValue method.
        transformType = (CameraTransformType)info.GetValue("transformType", typeof(CameraTransformType));
        smoothnessEnabled = (bool)info.GetValue("smoothnessEnabled", typeof(bool));

        List<float> startPositionList = (List<float>)info.GetValue("startPosition", typeof(List<float>));
        List<float> endPositionList = (List<float>)info.GetValue("startPosition", typeof(List<float>));
        List<float> startRotationList = (List<float>)info.GetValue("startPosition", typeof(List<float>));
        List<float> endRotationList = (List<float>)info.GetValue("startPosition", typeof(List<float>));

        startPosition = ListToNullableVector(startPositionList);
        endPosition = ListToVector(endPositionList);
        startRotation = ListToNullableVector(startRotationList);
        endRotation = ListToVector(endRotationList);
        duration = (float)info.GetValue("duration", typeof(float));
    }
}

[Serializable]
public class SerializableLinearTransform
{
    public string transformType { get; set; }
    public bool smoothnessEnabled { get; set; }

    public List<float> startPosition { get; set; }
    public List<float> endPosition { get; set; }
    public List<float> startRotation { get; set; }
    public List<float> endRotation { get; set; }
    public float duration { get; set; }
}


[Serializable]
public class LinearTransformParseError : Exception {
    public LinearTransformParseError() { }

    public LinearTransformParseError(string message) : base(message) { }
}