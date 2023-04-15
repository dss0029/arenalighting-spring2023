using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class CameraSequence: ISerializable
{
    public string sequenceName { get; set; }
    public List<ICameraMovement> sequences { get; set; }

    public CameraSequence()
    {
        sequenceName = "New Sequence";
        sequences = new List<ICameraMovement>();
    }

    public CameraSequence(string sequenceName, List<ICameraMovement> sequences)
    {
        this.sequenceName = sequenceName;
        this.sequences = sequences;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        // Use the AddValue method to specify serialized values.
        info.AddValue("sequenceName", sequenceName, typeof(string));
        info.AddValue("sequences", sequences, typeof(List<ICameraMovement>));
    }

    // The special constructor is used to deserialize values.
    public CameraSequence(SerializationInfo info, StreamingContext context)
    {
        // Reset the property value using the GetValue method.
        sequenceName = (string)info.GetValue("sequenceName", typeof(string));
        sequences = (List<ICameraMovement>)info.GetValue("sequences", typeof(List<ICameraMovement>));
    }

    
    public Dictionary<string, object> toDict() {
        List<Dictionary<string, object>> sequencesDictionaryList = new List<Dictionary<string, object>>();

        foreach (ICameraMovement cameraMovement in sequences)
        {
            sequencesDictionaryList.Add(cameraMovement.toDict());
        }

        Dictionary<string, object> cameraSequenceDictionary = new Dictionary<string, object>()
        {
            {"sequenceName", sequenceName},
            {"sequences", sequencesDictionaryList},
        };


        return cameraSequenceDictionary;
    }

    public static CameraSequence fromDict(Dictionary<string, object> dict)
    {
        List<ICameraMovement> newSequences = new List<ICameraMovement>();

        Debug.Log("CameraSequence - fromDict");

        List<object> sequencesDictionaryList = (List<object>)dict["sequences"];

        Debug.Log("CameraSequence - Parsed sequencesDictionaryList");

        foreach (Dictionary<string, object> cameraMovementDict in sequencesDictionaryList) {
            Debug.Log("CameraSequence - Foreach");

            if ((string)cameraMovementDict["transformType"] == "Linear")
            {
                newSequences.Add(LinearTransform.staticFromDict(cameraMovementDict));
            }
            else if ((string)cameraMovementDict["transformType"] == "Circullar")
            {
                newSequences.Add(CircullarTransform.staticFromDict(cameraMovementDict));
            }
        }

        string sequenceName = (string)dict["sequenceName"];

        CameraSequence newCameraSequence = new CameraSequence(sequenceName, newSequences);

        return newCameraSequence;
    }
}

[Serializable]
public class CameraSequenceParseError : Exception
{
    public CameraSequenceParseError() { }

    public CameraSequenceParseError(string message) : base(message) { }
}