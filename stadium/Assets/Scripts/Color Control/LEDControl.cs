using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LEDControl : MonoBehaviour
{
    public Button auBlue1;
    public Button auBlue2;
    public Button auOrange1;
    public Button auOrange2;

    // Start is called before the first frame update
    void Start()
    {
        Color startCol;
        // string htmlValue = "#03244d";
        Renderer rend = GetComponent<Renderer>();

        startCol = new Color(0.2862745f, 0.4313726f, 0.6117647f, .5f);
        rend.material.color = startCol;
        rend.material.SetColor("_EmissionColor", startCol);

        auBlue1.onClick.AddListener(SetBlue1);
        auBlue2.onClick.AddListener(SetBlue2);
        auOrange1.onClick.AddListener(SetOrange1);
        auOrange2.onClick.AddListener(SetOrange2);


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SetBlue1()
    {
        Color newCol;
        Renderer rend = GetComponent<Renderer>();
        // string htmlValue = "#03244d";
        newCol = new Color(0.01176471f, 0.1411765f, 0.3019608f, .5f);
        rend.material.color = newCol;
        rend.material.SetColor("_EmissionColor", newCol);

    }

    void SetBlue2()
    {
        Color newCol;
        Renderer rend = GetComponent<Renderer>();
        // string htmlValue = "#03244d";
        newCol = new Color(0.2862745f, 0.4313726f, 0.6117647f, .5f);
        rend.material.color = newCol;
        rend.material.SetColor("_EmissionColor", newCol);
    }

    void SetOrange1()
    {
        Color newCol;
        Renderer rend = GetComponent<Renderer>();
        // string htmlValue = "#03244d";
        newCol = new Color(0.8666667f, 0.3333333f, 0.04705882f, .5f);
        rend.material.color = newCol;
        rend.material.SetColor("_EmissionColor", newCol);
    }

    void SetOrange2()
    {
        Color newCol;
        Renderer rend = GetComponent<Renderer>();
        // string htmlValue = "#03244d";
        newCol = new Color(0.9647059f, 0.5019608f, 0.1490196f, .5f);
        rend.material.color = newCol;
        rend.material.SetColor("_EmissionColor", newCol);
    }
}
