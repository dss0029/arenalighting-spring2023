using UnityEngine;
using UnityEngine.UI;

public class InputReader : MonoBehaviour 
{
    public GameObject LEDTemplate;
    public Toggle fadeToggle;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    public bool fading;
    public Color fadeEnd;
    public float fadeFrame;
    public Button submitButton;
    public Color inputColor;
    public bool badInput;

    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        input.onEndEdit.AddListener(SubmitName);  // This also works
        submitButton.onClick.AddListener(buttonPress);
        badInput = true;

    }

    void Update()
    {
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        if (fading)
        {
            fadeFrame = fadeFrame + 0.1f;
            Color frameColor = gradient.Evaluate(fadeFrame);
            foreach (GameObject LED in allLEDs)
            {
                LED.GetComponent<Renderer>().material.color = frameColor;
                LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", frameColor);
            }
            Color currentColor = LEDTemplate.GetComponent<Renderer>().material.color;
            if (currentColor == fadeEnd || fadeFrame > 2.0f)
            {
                fading = false;
                Debug.Log("Done");
            }
        }
    }

    void buttonPress()
    {
        if (!badInput)
        {
            Color newCol;
            string tag;
            GameObject[] allLEDs;
            tag = "LED";
            allLEDs = GameObject.FindGameObjectsWithTag(tag);
            newCol = inputColor;
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
                fading = true;
            }
        }
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
            inputColor = newCol;
            badInput = false;
        }
        else
        {
            Debug.Log("Error: " + arg0 + " is not a valid hexadecimal value.");
            badInput = true;
        }
    }
}