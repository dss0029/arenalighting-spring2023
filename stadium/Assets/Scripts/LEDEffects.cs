using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LEDEffects : MonoBehaviour
{

    public GameObject[] allLEDs;
    public string tag;
    public string LEDcolor;
    void Start()
    {
        Color startCol;
        // string htmlValue = "#03244d";
        string tag;
        GameObject[] allLEDs;
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        startCol = new Color(0.2862745f, 0.4313726f, 0.6117647f, .5f);
        foreach (GameObject LED in allLEDs)
        {
            LED.GetComponent<Renderer>().material.color = startCol;
            LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", startCol);
        }
        LEDcolor = "blue";
    }

    // Update is called once per frame
    void Update()
    {
        tag = "LED";
        Color newCol;
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        if (Input.GetKey(KeyCode.X))
            {
                if (LEDcolor == "blue")
                {
                    newCol = new Color(0.8666667f, 0.3333333f, 0.04705882f, .5f);
                    foreach (GameObject LED in allLEDs)
                    {
                        LED.GetComponent<Renderer>().material.color = newCol;
                        LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", newCol);
                    }
                LEDcolor = "orange";
                }
                else
                {
                    newCol = new Color(0.2862745f, 0.4313726f, 0.6117647f, .5f);
                    foreach (GameObject LED in allLEDs)
                    {
                        LED.GetComponent<Renderer>().material.color = newCol;
                        LED.GetComponent<Renderer>().material.SetColor("_EmissionColor", newCol);
                    }
                LEDcolor = "blue";
                }
            }
    }
}
