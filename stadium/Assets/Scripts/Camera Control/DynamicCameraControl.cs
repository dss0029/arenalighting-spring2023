using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicCameraControl : MonoBehaviour
{
    public TMP_Text dynamicViewText;
    public CameraController cameraController;

    private int currentSelection = 0;
    private List<string> dynamicViews = new List<string> { "Aerial View" };


    private void Start()
    {

        UpdateUI(dynamicViews[currentSelection]);
    }

    public void ChangePositionRight()
    {
        if (currentSelection + 1 == dynamicViews.Count)
        {
            currentSelection = 0;
        }
        else
        {
            currentSelection += 1;
        }

        UpdateUI(dynamicViews[currentSelection]);
    }

    public void ChangePositionLeft()
    {
        if (currentSelection == 0)
        {
            currentSelection = dynamicViews.Count - 1;
        }
        else
        {
            currentSelection -= 1;
        }

        UpdateUI(dynamicViews[currentSelection]);
    }

    private void UpdateUI(string selectionName)
    {
        dynamicViewText.text = selectionName;
    }
}
