using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public bool glowing;
    Material emissiveMaterial;
    public Button sectionSelectorButton;
    public InputField sectionSelectorInput;

    public List<Transform> GetAllChildren(Transform LEDs, bool recursive)
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in LEDs)
        {
            children.Add(child);
            if (recursive)
            {
                children.AddRange(GetAllChildren(child, true));
            }
        }
        return children;
    }

    void changeSectionLights(string[] sections)
    {
        glowing = !glowing;
        foreach (string section in sections)
        {
            GameObject selectedSection = GameObject.Find(section);
            Debug.Log(selectedSection);
            Transform[] selectedSectionLights = selectedSection.GetComponentsInChildren<Transform>();
            List<Transform> lightsList = new List<Transform>();
            foreach (Transform t in selectedSectionLights)
            {
                if (t.name.Contains("Sphere"))
                {
                    lightsList.Add(t.transform);
                    emissiveMaterial = t.GetComponent<Renderer>().material;
                    if (glowing)
                    {
                        emissiveMaterial.DisableKeyword("_EMISSION");
                    }
                    else
                    {
                        emissiveMaterial.EnableKeyword("_EMISSION");
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        glowing = true;
        string[] sections = new string[]{"LEDs (1)", "LEDs"};
        sectionSelectorButton.onClick.AddListener(delegate{changeSectionLights(sections);});
        //changeSectionLights(section);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
