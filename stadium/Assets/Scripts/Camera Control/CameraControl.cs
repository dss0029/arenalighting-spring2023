using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    private int currentSelectedType = 0;
    private List<string> cameraViewModes = new List<string> { "Fixed", "Dynamic", "Free" };

    public TMP_Text cameraControlTypeText;
    public GameObject fixedCameraPositionPanel;
    public GameObject dynamicCameraControlPanel;
    public GameObject freeCameraControl;

    private void Start()
    {
        UpdateUI(cameraViewModes[currentSelectedType]);
    }

    public void ControlTypeLeft()
    {
        if (currentSelectedType == 0)
        {
            currentSelectedType = cameraViewModes.Count - 1;
        } else
        {
            currentSelectedType -= 1;
        }

        UpdateUI(cameraViewModes[currentSelectedType]);
    }

    public void ControlTypeRight()
    {
        if (currentSelectedType == cameraViewModes.Count - 1)
        {
            currentSelectedType = 0;
        } else
        {
            currentSelectedType += 1;
        }

        UpdateUI(cameraViewModes[currentSelectedType]);
    }

    private void UpdateUI(string cameraViewMode)
    {
        cameraControlTypeText.text = cameraViewMode;

        if (cameraViewMode == "Fixed")
        {
            DisableDynamicMode();
            DisableFreeMode();
            EnableFixedMode();
        }
        else if (cameraViewMode == "Dynamic")
        {
            DisableFixedMode();
            DisableFreeMode();
            EnableDynamicMode();
        }
        else
        {
            DisableFixedMode();
            DisableDynamicMode();
            EnableFreeMode();
        }
    }

    private void EnableFixedMode()
    {
        fixedCameraPositionPanel.SetActive(true);
    }

    private void DisableFixedMode()
    {
        fixedCameraPositionPanel.SetActive(false);
    }

    private void EnableDynamicMode()
    {
        dynamicCameraControlPanel.SetActive(true);
    }

    private void DisableDynamicMode()
    {
        dynamicCameraControlPanel.SetActive(false);
    }

    private void EnableFreeMode()
    {
        freeCameraControl.SetActive(true);
    }

    private void DisableFreeMode()
    {
        freeCameraControl.SetActive(false);
    }
}
