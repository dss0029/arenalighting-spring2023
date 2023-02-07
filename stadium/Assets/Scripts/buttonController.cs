// To use this example, attach this script to an empty GameObject.
// Create three buttons (Create>UI>Button). Next, select your
// empty GameObject in the Hierarchy and click and drag each of your
// Buttons from the Hierarchy to the Your First Button, Your Second Button
// and Your Third Button fields in the Inspector.
// Click each Button in Play Mode to output their message to the console.
// Note that click means press down and then release.

using UnityEngine;
using UnityEngine.UI;

public class buttonController : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button auBlue1;
    public Button auBlue2;
    public Button auOrange1;
    public Button auOrange2;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        auBlue1.onClick.AddListener(SetBlue1);
        auBlue2.onClick.AddListener(SetBlue2);
        auOrange1.onClick.AddListener(SetOrange1);
        auOrange2.onClick.AddListener(SetOrange2);
    }

    void SetBlue1()
    {
        Color newCol;
        // string htmlValue = "#03244d";
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        newCol = new Color(0.01176471f, 0.1411765f, 0.3019608f, 1f);
        foreach (GameObject LED in allLEDs)
        {
            LED.GetComponent<Renderer>().material.color = newCol;
        }
    }

    void SetBlue2()
    {
        Color newCol;
        // string htmlValue = "#03244d";
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        newCol = new Color(0.2862745f, 0.4313726f, 0.6117647f, 1f);
        foreach (GameObject LED in allLEDs)
        {
            LED.GetComponent<Renderer>().material.color = newCol;
        }
    }

    void SetOrange1()
    {
        Color newCol;
        // string htmlValue = "#03244d";
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        newCol = new Color(0.8666667f, 0.3333333f, 0.04705882f, .0f);
        foreach (GameObject LED in allLEDs)
        {
            LED.GetComponent<Renderer>().material.color = newCol;
        }
    }

    void SetOrange2()
    {
        Color newCol;
        // string htmlValue = "#03244d";
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        newCol = new Color(0.9647059f, 0.5019608f, 0.1490196f, .0f);
        foreach (GameObject LED in allLEDs)
        {
            LED.GetComponent<Renderer>().material.color = newCol;
        }
    }


}
