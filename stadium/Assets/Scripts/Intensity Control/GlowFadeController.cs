using UnityEngine;
using UnityEngine.UI;

public class GlowFadeController : MonoBehaviour
{
    public Button fadeButton;
    public Button glowButton;

    Material emissiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        fadeButton.onClick.AddListener(Fade);
        glowButton.onClick.AddListener(Glow);
    }

    void Fade()
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

    void Glow()
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
}
