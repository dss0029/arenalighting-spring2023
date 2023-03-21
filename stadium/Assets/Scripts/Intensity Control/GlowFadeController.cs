using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowFadeController : MonoBehaviour
{
    public Button fadeButton;
    public Button glowButton;
    public Button flashButton;
    // public Button pulseButton;
    // public Toggle flashToggle;
    public Toggle pulseToggle;


    public float pulseSpeed = 1.0f;
    public AnimationCurve BrightnessCurve;
    public Color startingColor;
    public bool flashing;

    Material emissiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        fadeButton.onClick.AddListener(fade);
        glowButton.onClick.AddListener(glow);
        flashButton.onClick.AddListener(flash);
        flashing = false;
    }

    void fade()
    {
        GameObject[] allLEDs;
        string tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject LED in allLEDs)
        {
            emissiveMaterial = LED.GetComponent<Renderer>().material;
            emissiveMaterial.DisableKeyword("_EMISSION");
        }
    }

    void glow()
    {
        GameObject[] allLEDs;
        string tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject LED in allLEDs)
        {
            emissiveMaterial = LED.GetComponent<Renderer>().material;
            emissiveMaterial.EnableKeyword("_EMISSION");
        }
    }

    void flash()
    {
        flashing = !flashing;
        if (flashing)
        {
            InvokeRepeating("fade", 0.0f, 1.0f);
            InvokeRepeating("glow", 0.5f, 1.0f);
        }
        else
        {
            CancelInvoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] allLEDs;
        string tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        

        if (pulseToggle.isOn)
        {
            float scaledTime = Time.time * pulseSpeed;

            foreach (GameObject LED in allLEDs)
            {
                emissiveMaterial = LED.GetComponent<Renderer>().material;
                startingColor = LED.GetComponent<Renderer>().material.color;

                float brightness = BrightnessCurve.Evaluate(scaledTime);
                Color color = startingColor;
                color = new Color(
                    color.r * Mathf.Pow(2, brightness),
                    color.g * Mathf.Pow(2, brightness),
                    color.b * Mathf.Pow(2, brightness),
                    color.a);

                emissiveMaterial.SetColor("_EmissionColor", color);
            }
        }

        /*
        else
        {
            foreach (GameObject LED in allLEDs)
            {
                startingColor = LED.GetComponent<Renderer>().material.color;
                emissiveMaterial = LED.GetComponent<Renderer>().material;
                emissiveMaterial.SetColor("_EmissionColor", startingColor * 0.03f);
            }
        }
        */
    }
}
