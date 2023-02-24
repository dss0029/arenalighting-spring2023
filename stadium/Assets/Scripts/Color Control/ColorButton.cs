// To use this example, attach this script to an empty GameObject.
// Create three buttons (Create>UI>Button). Next, select your
// empty GameObject in the Hierarchy and click and drag each of your
// Buttons from the Hierarchy to the Your First Button, Your Second Button
// and Your Third Button fields in the Inspector.
// Click each Button in Play Mode to output their message to the console.
// Note that click means press down and then release.

using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button colorSetButton;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        colorSetButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Color newCol;
        string htmlValue = "#FF0000";
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);

        if (ColorUtility.TryParseHtmlString(htmlValue, out newCol))
        {
            foreach (GameObject LED in allLEDs)
            {
                LED.GetComponent<Renderer>().material.color = newCol;
            }
        }
    }


}
