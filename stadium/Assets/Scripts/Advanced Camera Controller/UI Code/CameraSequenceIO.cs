using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CameraSequenceIO : MonoBehaviour
{
    [SerializeField]
    private TMP_Text sequenceNameText;

    [SerializeField]
    private AdvancedCameraController advancedCameraController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static T LoadJson<T>()
    {
        string path = EditorUtility.OpenFilePanel("Select Camera Sequence File", "", "json");
        if (path.Length != 0)
        {
            return ImportJson<T>(path);
        }

        return default(T);
    }

    public static void SaveJson(string filename, object obj)
    {
        var path = EditorUtility.SaveFilePanel(
            "Save camera sequence as JSON",
            "",
            filename + ".json",
            "json");

        if (path.Length != 0)
        {
            ExportJson(path, obj);
        }
    }

    public static T ImportJson<T>(string path)
    {
        var fileContent = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(fileContent);
    }

    public static void ExportJson(string path, object obj)
    {
        var jsonText = JsonConvert.SerializeObject(obj);
        File.WriteAllText(path, jsonText);
    }


    public void LoadCameraSequence()
    {
        Dictionary<string, object> cameraSequenceDict = LoadJson<Dictionary<string, object>>();
        Debug.Log("CameraSequence - Parsed sequencesDictionaryList" + cameraSequenceDict.ToString());

        CameraSequence cameraSequence = CameraSequence.fromDict(cameraSequenceDict);
        Debug.Log("Loaded Sequnce: " + cameraSequence.sequenceName);
        advancedCameraController.UpdateSequence(cameraSequence);
        sequenceNameText.text = cameraSequence.sequenceName;
    }

    public void SaveCameraSequence()
    {
        CameraSequence sequence = advancedCameraController.cameraSequence;

        Debug.Log("SaveCameraSequence Update: " + sequence.sequences.Count);
        SaveJson("New Sequence", sequence);
    }
}
