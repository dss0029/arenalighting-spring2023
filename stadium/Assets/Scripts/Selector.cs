using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
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

    void getSectionLights(string[] sections)
    {
        foreach (string section in sections)
        {
            GameObject selectedSection = GameObject.Find(section);
            //Debug.Log(selectedSection);
            Transform[] selectedSectionLights = selectedSection.GetComponentsInChildren<Transform>();
            List<Transform> lightsList = new List<Transform>();
            foreach (Transform light in selectedSectionLights)
            {
                if (light.name.Contains("LEDTemplate"))
                {
                    lightsList.Add(light.transform);
                    //Debug.Log(light);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string[] sections = new string[]{"Section 16", "Section 19"};
        getSectionLights(sections);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
