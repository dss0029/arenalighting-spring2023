// To use this example, attach this script to an empty GameObject.
// Create three buttons (Create>UI>Button). Next, select your
// empty GameObject in the Hierarchy and click and drag each of your
// Buttons from the Hierarchy to the Your First Button, Your Second Button
// and Your Third Button fields in the Inspector.
// Click each Button in Play Mode to output their message to the console.
// Note that click means press down and then release.

using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button auBlue1;
    public Button auBlue2;
    public Button auOrange1;
    public Button auOrange2;
    public Button randomButton;
    public GameObject LEDTemplate;
    public Toggle fadeToggle;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    public bool fading;
    public Color fadeEnd;
    public float fadeFrame;
    public InputField speedInput;
    public float fadeDuration;
    public float fadeTime;
    public Toggle randomToggle;
    public bool randomizing;

    void Start()
    {
        fadeFrame = 0.0f;
        fadeDuration = 1.0f;
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        auBlue1.onClick.AddListener(SetBlue1);
        auBlue2.onClick.AddListener(SetBlue2);
        auOrange1.onClick.AddListener(SetOrange1);
        auOrange2.onClick.AddListener(SetOrange2);
        randomButton.onClick.AddListener(setRandom);
        speedInput.onEndEdit.AddListener(setSpeed);
        randomizing = false;
    }

    void Update()
    {
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        if (randomToggle.isOn)
        {
            if (!randomizing)
            {
                InvokeRepeating("setRandom", 0.0f, 0.5f);
                randomizing = true;
            } 
        }
        else
        {
            CancelInvoke();
            randomizing = false;
        }

        if (fading)
        {
            fadeTime += Time.deltaTime;
            fadeFrame = fadeTime / fadeDuration;
            Color frameColor = gradient.Evaluate(fadeFrame);
            foreach (GameObject LED in allLEDs)
            {
                LED.GetComponent<Renderer>().material.color = frameColor;
                LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", frameColor);
            }
            Color currentColor = LEDTemplate.GetComponent<Renderer>().material.color;
            if (currentColor == fadeEnd)
            {
                fading = false;
            }
        }
    }

    void setSpeed(string arg0)
    {
        fadeDuration = float.Parse(arg0);
    }

    void SetBlue1()
    {
        Color newCol;
        // string htmlValue = "#03244d";
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        newCol = new Color(0.012f, 0.141f, 0.302f, .500f);
        if (!fadeToggle.isOn)
        {
         
            foreach (GameObject LED in allLEDs)
            {
                LED.GetComponent<Renderer>().material.color = newCol;
                LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", newCol);
            }
           
        }
        else {
            Color startCol = LEDTemplate.GetComponent<Renderer>().material.color;
            Color endCol = newCol;
            gradient = new Gradient();

            colorKey = new GradientColorKey[2];
            colorKey[0].color = startCol;
            colorKey[0].time = 0.0f;
            colorKey[1].color = endCol;
            colorKey[1].time = fadeDuration;

            alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 0.5f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.5f;
            alphaKey[1].time = fadeDuration;

            gradient.SetKeys(colorKey, alphaKey);
            fadeEnd = endCol;
            fadeFrame = 0.0f;
            fadeTime = 0.0f;
            fading = true;
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
        newCol = new Color(0.2862745f, 0.4313726f, 0.6117647f, .5f);
        if (!fadeToggle.isOn)
        {
            foreach (GameObject LED in allLEDs)
            {
                LED.GetComponent<Renderer>().material.color = newCol;
                LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", newCol);
            }
        }
        else
        {
            Color startCol = LEDTemplate.GetComponent<Renderer>().material.color;
            Color endCol = newCol;
            gradient = new Gradient();

            colorKey = new GradientColorKey[2];
            colorKey[0].color = startCol;
            colorKey[0].time = 0.0f;
            colorKey[1].color = endCol;
            colorKey[1].time = 1.0f;

            alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 0.5f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.5f;
            alphaKey[1].time = 1.0f;

            gradient.SetKeys(colorKey, alphaKey);
            fadeEnd = endCol;
            fadeFrame = 0.0f;
            fadeTime = 0.0f;
            fading = true;
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
        newCol = new Color(0.8666667f, 0.3333333f, 0.04705882f, .5f);
        if (!fadeToggle.isOn)
        {
            foreach (GameObject LED in allLEDs)
            {
                LED.GetComponent<Renderer>().material.color = newCol;
                LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", newCol);
            }
        }
        else
        {
            Color startCol = LEDTemplate.GetComponent<Renderer>().material.color;
            Color endCol = newCol;
            gradient = new Gradient();

            colorKey = new GradientColorKey[2];
            colorKey[0].color = startCol;
            colorKey[0].time = 0.0f;
            colorKey[1].color = endCol;
            colorKey[1].time = 1.0f;

            alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 0.5f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.5f;
            alphaKey[1].time = 1.0f;

            gradient.SetKeys(colorKey, alphaKey);
            fadeEnd = endCol;
            fadeFrame = 0.0f;
            fadeTime = 0.0f;
            fading = true;
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
        newCol = new Color(0.9647059f, 0.5019608f, 0.1490196f, .5f);
        if (!fadeToggle.isOn)
        {
            foreach (GameObject LED in allLEDs)
            {
                LED.GetComponent<Renderer>().material.color = newCol;
                LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", newCol);
            }
        }
        else
        {
            Color startCol = LEDTemplate.GetComponent<Renderer>().material.color;
            Color endCol = newCol;
            gradient = new Gradient();

            colorKey = new GradientColorKey[2];
            colorKey[0].color = startCol;
            colorKey[0].time = 0.0f;
            colorKey[1].color = endCol;
            colorKey[1].time = 1.0f;

            alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 0.5f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.5f;
            alphaKey[1].time = 1.0f;

            gradient.SetKeys(colorKey, alphaKey);
            fadeEnd = endCol;
            fadeFrame = 0.0f;
            fadeTime = 0.0f;
            fading = true;
        }
    }

    void setRandom()
    {
        Color newCol;
        // string htmlValue = "#03244d";
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        // newCol = new Color(0.9647059f, 0.5019608f, 0.1490196f, .5f);
        foreach (GameObject LED in allLEDs)
        {
            int randomCol = Random.Range(1, 6);
            if (randomCol < 3)
            {
                newCol = new Color(0.2862745f, 0.4313726f, 0.6117647f, .5f);
            }
            else if (randomCol > 4)
            {
                newCol = new Color(1.0f, 1.0f, 1.0f, .5f);
            }
            else
            {
                newCol = new Color(0.8666667f, 0.3333333f, 0.04705882f, .5f);
            }
            LED.GetComponent<Renderer>().material.color = newCol;
            LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", newCol);
        }
    }
}

