using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Huge thanks to Peer Play [https://www.youtube.com/@PeerPlay]

public class MusicLightController : MonoBehaviour
{
    public AudioPeer audioPeer;

    public float minScale;
    public float maxScale;
    public bool useBuffer;

    private GameObject[] allLEDs;
    // Start is called before the first frame update
    void Start()
    {
        tag = "LED";
        allLEDs = GameObject.FindGameObjectsWithTag(tag);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allLEDs.Length; i++)
        {
            float scaleValue;
            if (useBuffer)
            {
                // Debug.Log(audioPeer.audioBandBuffer[1]);
                scaleValue = (audioPeer.audioBandBuffer[i % 8] * (maxScale - minScale)) + minScale;
            }
            else
            {
                scaleValue = (audioPeer.audioBand[i % 8] * (maxScale - minScale)) + minScale;
            }

            // Transform current led
            //Transform currentLedTransform = allLEDs[i].GetComponent<Transform>();
            //currentLedTransform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

            // Change transparency of the led

            Color currentLedColor = allLEDs[i].GetComponent<Renderer>().material.color;
            Color newLedColor = new Color(currentLedColor.r, currentLedColor.g, currentLedColor.b, (audioPeer.audioBand[i % 8]));

            allLEDs[i].GetComponent<Renderer>().material.color = newLedColor;
            allLEDs[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", newLedColor);

        }
    }
}
