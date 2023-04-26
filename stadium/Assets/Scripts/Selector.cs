using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
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
        bool selector = false;
        if (selector == true)
        {
            string[] sections = new string[]{""};
            // "Section 16, Section 19" for example
            getSectionLights(sections);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
