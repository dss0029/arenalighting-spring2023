using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicToLightModeController : MonoBehaviour
{
    public TMP_Text musicToLightModeText;
    private int currentSelectedMode = 0;
    private List<string> musicToLightModes = new List<string> { "Linear", "Random", "Amplitude" };

    private void Start()
    {
        UpdateUI(musicToLightModes[currentSelectedMode]);
    }

    public string getCurrentMode()
    {
        return musicToLightModes[currentSelectedMode];
    }

    public void ControlTypeLeft()
    {
        if (currentSelectedMode == 0)
        {
            currentSelectedMode = musicToLightModes.Count - 1;
        }
        else
        {
            currentSelectedMode -= 1;
        }

        UpdateUI(musicToLightModes[currentSelectedMode]);
    }

    public void ControlTypeRight()
    {
        if (currentSelectedMode == musicToLightModes.Count - 1)
        {
            currentSelectedMode = 0;
        }
        else
        {
            currentSelectedMode += 1;
        }

        UpdateUI(musicToLightModes[currentSelectedMode]);
    }

    private void UpdateUI(string cameraViewMode)
    {
        musicToLightModeText.text = cameraViewMode;
    }
}
