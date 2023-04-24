using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovementPanelManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text numberText;
    [SerializeField]
    TMP_Text movementNameText;
    [SerializeField]
    TMP_Dropdown movementTypeDropDown;

    int movementID;
    ICameraMovement cameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        numberText.text = "#";
        movementNameText.text = "Movement name";
        InitializeCameraMovementTypeOptions();
    }

    // Update is called once per frame
    void Update()
    {
        numberText.text = (movementID + 1).ToString();
        movementNameText.text = cameraMovement.movementName;
        InitializeCameraMovementTypeOptions();
        movementTypeDropDown.value = movementTypeDropDown.options.FindIndex(option => option.text == cameraMovement.transformType.ToString());
    }

    void InitializeCameraMovementTypeOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        foreach (string cameraTransformType in Enum.GetNames(typeof(CameraTransformType)))
        {
            options.Add(new TMP_Dropdown.OptionData(cameraTransformType));
        }

        movementTypeDropDown.options = options;
    }

    public void UpdateCameraMovementPanel(int movementID, ICameraMovement cameraMovement)
    {
        this.movementID = movementID;
        this.cameraMovement = cameraMovement;
    }
}
