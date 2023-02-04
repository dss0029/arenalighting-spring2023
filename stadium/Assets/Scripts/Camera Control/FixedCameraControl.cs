using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FixedCameraControl : MonoBehaviour
{
    public TMP_Text fixedPositionNameText;
    public Camera MainCamera;

    public Vector3 startPosition;
    public Vector3 startRotation;
    public float speed;

    private Dictionary<string, Tuple<Vector3, Vector3>> positions =
        new Dictionary<string, Tuple<Vector3, Vector3>>();
    private int currentPosition;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private void Start()
    {
        Vector3 rotationVector = new Vector3(10f, -90f, 0);
        positions.Add("Default", new Tuple<Vector3, Vector3>(startPosition, startRotation));
        positions.Add("Section 28", new Tuple<Vector3, Vector3>(new Vector3(-8.97f, 3.0f, -10.02f), rotationVector));
        positions.Add("Section 29", new Tuple<Vector3, Vector3>(new Vector3(-9.02f, 3.0f, -4.34f), rotationVector));
        // positions.Add("Section 29", new Tuple<Vector3, Vector3>(new Vector3(-8.97f, 3.0f, -10.02f), rotationVector));


        List<string> keyList = new List<string>(this.positions.Keys);
        UpdateUI(keyList[currentPosition]);
        MoveTo(positions["Default"].Item1, positions["Default"].Item2);
    }

    private void Update()
    {
        MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, targetPosition, speed * Time.deltaTime);
        MainCamera.transform.rotation = Quaternion.Lerp(MainCamera.transform.rotation, targetRotation, speed * Time.deltaTime);
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
        MoveTo(transformData.Item1, transformData.Item2);
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
        MoveTo(transformData.Item1, transformData.Item2);
    }

    private void UpdateUI(string positionName)
    {
        fixedPositionNameText.text = positionName;
    }

    public void MoveTo(Vector3 pos, Vector3 rotationVector)
    {
        targetPosition = pos;
        targetRotation = Quaternion.Euler(rotationVector);
    }
}
