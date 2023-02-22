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
        fadeButton.onClick.AddListener(fade);
        glowButton.onClick.AddListener(glow);
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
}
