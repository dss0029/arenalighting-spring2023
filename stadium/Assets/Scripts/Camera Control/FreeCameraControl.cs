using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraControl : MonoBehaviour
{
    public Camera MainCamera;

    public float rotationSensitivity;
    public float rotationX = 0f;
    public float rotationY = 0f;


    public float movementSpeed;

    // Update is called once per frame
    void Update()
    {

        float x_movement = 0f;
        float y_movement = 0f;
        float z_movement = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            z_movement += movementSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            z_movement -= movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x_movement -= movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x_movement += movementSpeed;
        }

        x_movement *= Time.deltaTime;
        z_movement *= Time.deltaTime;


        // Move object relative to its position
        MainCamera.transform.Translate(x_movement, y_movement, z_movement, Space.Self);


        // Rotate object using a mouse
        rotationX += Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -50, 50);
        MainCamera.transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);

    }
}
