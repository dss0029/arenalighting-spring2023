using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraSequence
{
    public string sequenceName { get; set; }
    public List<ICameraMovement> sequences { get; set; }

    public CameraSequence()
    {
        sequenceName = "New Camera Sequence";
        sequences = new List<ICameraMovement>();
    }

    public CameraSequence(string sequenceName, List<ICameraMovement> sequences)
    {
        this.sequenceName = sequenceName;
        this.sequences = sequences;
    }

    public string ToJson(bool prettyPrint)
    {
        string[] sequencesJson = new string[sequences.Count];

        for(int i = 0; i < sequences.Count; i++)
        {
            sequencesJson[i] = sequences[i].ToJson(prettyPrint);
        }

        SerializableCameraSequence serializableCameraSequence = new SerializableCameraSequence();
        serializableCameraSequence.sequenceName = sequenceName;
        serializableCameraSequence.sequences = JsonHelper.ToJson(sequencesJson, prettyPrint);

        serializableCameraSequence.sequenceCount = sequences.Count;
        
        string cameraSequnceJson = JsonUtility.ToJson(serializableCameraSequence, prettyPrint);
        return cameraSequnceJson;
    }

    public static CameraSequence FromJson(string json)
    {
        SerializableCameraSequence serializableCameraSequence = JsonUtility.FromJson<SerializableCameraSequence>(json);
        string sequenceName = serializableCameraSequence.sequenceName;
        string[] sequencesJson = JsonHelper.FromJson<string>(serializableCameraSequence.sequences);

        List<ICameraMovement> cameraMovements = new List<ICameraMovement>();

        for (int i = 0; i < serializableCameraSequence.sequenceCount; i++)
        {
            if (sequencesJson[i].Contains("Linear"))
            {
                cameraMovements.Add(LinearMovement.FromJson(sequencesJson[i]));
            }
            else if (sequencesJson[i].Contains("Circullar"))
            {
                cameraMovements.Add(CircullarMovement.FromJson(sequencesJson[i]));
            }
        }

        return new CameraSequence(sequenceName, cameraMovements);
    }
}

[Serializable]
public class SerializableCameraSequence
{
    public string sequenceName;
    public string sequences;
    public int sequenceCount;
}

[Serializable]
public class CameraSequenceParseError : Exception
{
    public CameraSequenceParseError() { }

    public CameraSequenceParseError(string message) : base(message) { }
}