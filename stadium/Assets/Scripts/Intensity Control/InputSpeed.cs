using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSpeed : MonoBehaviour
{
    public InputField inputSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeEffectSpeed(string input)
    {
        float newSpeed;
        newSpeed = float.Parse(input);
        inputSpeed.GetComponent<GlowFadeController>().pulseSpeed = newSpeed;
    }

}


