using UnityEngine;
using UnityEngine.UI;

public class GlowFadeController : MonoBehaviour
{
    public Button fadeButton;
    public Button glowButton;
    public Button flashButton;
    public Toggle pulseToggle;


    public float pulseSpeed;
    public AnimationCurve BrightnessCurve;
    public Color startingColor;
    public bool flashing;
    public InputField pulseSpeedInput;
    Material emissiveMaterial;

    GameObject[] GetAllLEDs()
    {
        string ledTag = "LED";
        return GameObject.FindGameObjectsWithTag(ledTag);
    }

    // Start is called before the first frame update
    void Start()
    {
        pulseSpeed = 1.0f;
        fadeButton.onClick.AddListener(Fade);
        glowButton.onClick.AddListener(Glow);
        flashButton.onClick.AddListener(Flash);
        pulseSpeedInput.onEndEdit.AddListener(SetPulseSpeed);
        flashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] allLEDs = GetAllLEDs();

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
    }

    void Fade()
    {
        GameObject[] allLEDs = GetAllLEDs();
        foreach (GameObject LED in allLEDs)
        {
            emissiveMaterial = LED.GetComponent<Renderer>().material;
            emissiveMaterial.DisableKeyword("_EMISSION");
        }
    }

    void Glow()
    {
        GameObject[] allLEDs = GetAllLEDs();
        foreach (GameObject LED in allLEDs)
        {
            emissiveMaterial = LED.GetComponent<Renderer>().material;
            emissiveMaterial.EnableKeyword("_EMISSION");
        }
    }

    void Flash()
    {
        flashing = !flashing;
        if (flashing)
        {
            InvokeRepeating("Fade", 0.0f, 1.0f);
            InvokeRepeating("Glow", 0.5f, 1.0f);
        }
        else
        {
            CancelInvoke();
        }
    }

    void SetPulseSpeed(string speed)
    {
        if (speed == null || speed == "") return;
        pulseSpeed = float.Parse(speed);
    }
}
