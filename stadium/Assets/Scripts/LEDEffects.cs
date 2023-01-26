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
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject LED in allLEDs)
        {
            LED.GetComponent<Renderer>().material.color = Color.blue;
        }
        LEDcolor = "blue";
    }

    // Update is called once per frame
    void Update()
    {
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
        if (Input.GetKey(KeyCode.X))
            {
                if (LEDcolor == "blue")
                {
                    foreach (GameObject LED in allLEDs)
                    {
                        LED.GetComponent<Renderer>().material.color = Color.green;
                    }
                LEDcolor = "green";
                }
                else
                {
                    foreach (GameObject LED in allLEDs)
                    {
                        LED.GetComponent<Renderer>().material.color = Color.blue;
                    }
                LEDcolor = "blue";
                }
            }
    }
}
