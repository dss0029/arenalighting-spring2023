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

    public static string LoadJson()
    {
        string path = EditorUtility.OpenFilePanel("Select Camera Sequence File", "", "json");
        if (path.Length != 0)
        {
            return ImportJson(path);
        }

        return null;
    }

    public static void SaveJson(string filename, string json)
    {
        var path = EditorUtility.SaveFilePanel(
            "Save camera sequence as JSON",
            "",
            filename + ".json",
            "json");

        if (path.Length != 0)
        {
            ExportJson(path, json);
        }
    }

    public static string ImportJson(string path)
    {
        string fileContent = File.ReadAllText(path);
        return fileContent;
    }

    public static void ExportJson(string path, string json)
    {
        File.WriteAllText(path, json);
    }


    public void LoadCameraSequence()
    {
        string cameraSequenceJson = LoadJson();
        if (cameraSequenceJson == null) return;

        CameraSequence cameraSequence = CameraSequence.FromJson(cameraSequenceJson);

        advancedCameraController.UpdateSequence(cameraSequence);
        sequenceNameText.text = cameraSequence.sequenceName;
    }

    public void SaveCameraSequence()
    {
        SaveJson("New Sequence", advancedCameraController.cameraSequence.ToJson(true));
    }
}
