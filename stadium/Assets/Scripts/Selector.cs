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

    /*public GameObject[] GetAllLEDs()
    {
        GameObject[] allLEDs;
        string tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);

        return allLEDs;
    }

    public List<GameObject> GetRow()
    {

    }*/

    public List<Transform> GetSection(Transform LEDs)
    {
        List<Transform> children = GetAllChildren(transform, false);

        foreach(Transform child in children)
        {
            GameObject[] allLeds;
            string tag = "LED";
            allLeds = GameObject.FindGameObjectsWithTag(tag);
            foreach(GameObject LED in allLeds)
            {
                children.Add(LED.transform);
            }
        }
        return children;
    }

    // Start is called before the first frame update
    void Start()
    {
        List<Transform> children = GetAllChildren(transform, false);

        foreach(Transform child in children)
        {
            GameObject[] allLeds;
            string tag = "LED";
            allLeds = GameObject.FindGameObjectsWithTag(tag);
            foreach(GameObject LED in allLeds)
            {
                Debug.Log(LED.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
