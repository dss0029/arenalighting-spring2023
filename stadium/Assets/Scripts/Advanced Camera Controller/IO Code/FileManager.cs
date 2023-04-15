using System.IO;
using UnityEditor;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static void LoadJson<T>()
    {
        string path = EditorUtility.OpenFolderPanel("Select Camera Sequence File", "", "json");
        if (path.Length != 0)
        {
            ImportJson<T>(path);
        }
    }

    public static void SaveJson(string filename, object obj)
    {
        var path = EditorUtility.SaveFilePanel(
            "Save camera sequence as JSON",
            "",
            filename + ".json",
            "png");

        if (path.Length != 0)
        {
            ExportJson(path, obj);
        }
    }

    public static T ImportJson<T>(string path)
    {
        var fileContent = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(fileContent);
    }

    public static void ExportJson(string path, object obj)
    {
        var jsonText = JsonUtility.ToJson(obj);
        File.WriteAllText(path, jsonText);
    }
}
