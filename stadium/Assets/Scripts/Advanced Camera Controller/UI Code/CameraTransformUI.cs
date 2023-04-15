using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraTransformUI : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    TMP_Text positionTextMeshPro;
    [SerializeField]
    TMP_Text rotationTextMeshPro;

    // Update is called once per frame
    void Update()
    {
        positionTextMeshPro.text = "Position: " + mainCamera.transform.position.ToString();
        rotationTextMeshPro.text = "Rotation: " + mainCamera.transform.rotation.eulerAngles.ToString();
    }
}
