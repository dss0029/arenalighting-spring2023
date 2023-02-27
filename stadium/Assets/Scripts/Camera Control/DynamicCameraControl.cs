using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicCameraControl : MonoBehaviour
{
    public TMP_Text dynamicViewText;

    public Camera MainCamera;
    public float speed = 3;
    public float inclineAngle = 30;

    private float rotationY;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget;

    private int currentSelection = 0;
    private List<string> dynamicViews = new List<string> { "Aerial View" };


    private void Start()
    {

        UpdateUI(dynamicViews[currentSelection]);
    }

    private void Update()
    {
        if (dynamicViews[currentSelection] == "Aerial View")
        {
            AerialView();
        }
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

    private void AerialView()
    {
        rotationY += speed * Time.deltaTime;

        MainCamera.transform.localEulerAngles = new Vector3(inclineAngle, rotationY, 0);

        MainCamera.transform.position = target.position - MainCamera.transform.forward * distanceFromTarget;
    }
}
