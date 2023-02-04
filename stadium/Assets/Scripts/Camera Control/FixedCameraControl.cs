using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FixedCameraControl : MonoBehaviour
{
    public TMP_Text fixedPositionNameText;
    public CameraController cameraController;

    private Dictionary<string, Tuple<Vector3, Vector3>> positions =
        new Dictionary<string, Tuple<Vector3, Vector3>>();
    private int currentPosition;

    private void Start()
    {
        Vector3 rotationVector = new Vector3(10f, -90f, 0);
        positions.Add("Default", new Tuple<Vector3, Vector3>(cameraController.startPosition, cameraController.startRotation));
        positions.Add("Section 28", new Tuple<Vector3, Vector3>(new Vector3(-8.97f, 3.0f, -10.02f), rotationVector));
        positions.Add("Section 29", new Tuple<Vector3, Vector3>(new Vector3(-9.02f, 3.0f, -4.34f), rotationVector));
        // positions.Add("Section 29", new Tuple<Vector3, Vector3>(new Vector3(-8.97f, 3.0f, -10.02f), rotationVector));


        List<string> keyList = new List<string>(this.positions.Keys);
        UpdateUI(keyList[currentPosition]);
        cameraController.MoveTo(positions["Default"].Item1, positions["Default"].Item2);
    }

    public void ChangePositionRight()
    {
        List<string> keyList = new List<string>(this.positions.Keys);
        if (currentPosition + 1 == keyList.Count)
        {
            currentPosition = 0;
        }
        else
        {
            currentPosition += 1;
        }

        string newPositionName = keyList[currentPosition];

        Tuple<Vector3, Vector3> transformData = positions[newPositionName];

        UpdateUI(newPositionName);
        cameraController.MoveTo(transformData.Item1, transformData.Item2);
    }

    public void ChangePositionLeft()
    {
        List<string> keyList = new List<string>(this.positions.Keys);
        if (currentPosition == 0)
        {
            currentPosition = keyList.Count - 1;
        }
        else
        {
            currentPosition -= 1;
        }

        string newPositionName = keyList[currentPosition];

        Tuple<Vector3, Vector3> transformData = positions[newPositionName];

        UpdateUI(newPositionName);
        cameraController.MoveTo(transformData.Item1, transformData.Item2);
    }

    private void UpdateUI(string positionName)
    {
        fixedPositionNameText.text = positionName;
    }
}
