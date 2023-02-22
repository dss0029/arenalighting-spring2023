using UnityEngine;
using UnityEngine.UI;

public class InputReader : MonoBehaviour
{
    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        input.onEndEdit.AddListener(SubmitName);  // This also works
    }

    private void SubmitName(string arg0)
    {
        Color newCol;
        string htmlValue = "#" + arg0;
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);

        if (ColorUtility.TryParseHtmlString(htmlValue, out newCol))
        {
            foreach (GameObject LED in allLEDs)
            {
                LED.GetComponent<Renderer>().material.color = newCol;
                LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", newCol);
            }
        }
        else
        {
            Debug.Log("Error: " + arg0 + " is not a valid hexadecimal value.");
        }
    }
}