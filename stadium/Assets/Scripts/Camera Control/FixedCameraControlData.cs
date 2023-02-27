using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;


public static class FixedCameraControlData
{
    public static string filePath = "FixedCameraControlData";

    public static string ToJson(Dictionary<string, Tuple<Vector3, Vector3>> fixedPositions)
    {
        Dictionary<string, Dictionary<string, Dictionary<string, float>>> serializedPositions =
            new Dictionary<string, Dictionary<string, Dictionary<string, float>>>();

        foreach(var position in fixedPositions)
        {
            Dictionary<string, float> positionDictionary = new Dictionary<string, float>();
            Dictionary<string, float> rotationDictionary = new Dictionary<string, float>();

            positionDictionary.Add("x", position.Value.Item1.x);
            positionDictionary.Add("y", position.Value.Item1.y);
            positionDictionary.Add("z", position.Value.Item1.z);

            rotationDictionary.Add("x", position.Value.Item2.x);
            rotationDictionary.Add("y", position.Value.Item2.y);
            rotationDictionary.Add("z", position.Value.Item2.z);

            Dictionary<string, Dictionary<string, float>> transformDictionary = new Dictionary<string, Dictionary<string, float>>();
            transformDictionary.Add("position", positionDictionary);
            transformDictionary.Add("rotation", rotationDictionary);

            serializedPositions.Add(position.Key, transformDictionary);
        }

        string jsonString = JsonConvert.SerializeObject(serializedPositions);

        return jsonString;
    }


    public static Dictionary<string, Tuple<Vector3, Vector3>> FromJson(string jsonString)
    {
        Dictionary<string, Dictionary<string, Dictionary<string, float>>> serializedPositions =
            JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, float>>>>(jsonString);

        Dictionary<string, Tuple<Vector3, Vector3>> fixedPositions = new Dictionary<string, Tuple<Vector3, Vector3>>();

        foreach (var data in serializedPositions)
        {

            Vector3 position = new Vector3(data.Value["position"]["x"], data.Value["position"]["y"], data.Value["position"]["z"]);
            Vector3 rotation = new Vector3(data.Value["rotation"]["x"], data.Value["rotation"]["y"], data.Value["rotation"]["z"]);

            fixedPositions.Add(data.Key, new Tuple<Vector3, Vector3>(position, rotation));
        }

        return fixedPositions;
    }

    public static Dictionary<string, Tuple<Vector3, Vector3>> LoadFixedCameraControlData()
    {
        Debug.Log("Opening text asset");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        Debug.Log(targetFile.name);
        return FromJson(targetFile.text);
    }
}
