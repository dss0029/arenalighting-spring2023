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
    [SerializeField]
    float normalSpeed, shiftSpeed, normalAcceleration, shiftAcceleration;

    void Start()
    {
        UpdateCurrentRotation();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeed();
        UpdateMovement();

        // Rotate object using a mouse
        rotationX += Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -50, 50);
        MainCamera.transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);

    }

    void UpdateCurrentRotation()
    {
        Transform cameraTransform = MainCamera.transform;
        rotationX = cameraTransform.localEulerAngles.x;
        rotationY = cameraTransform.localEulerAngles.y;
    }

    void UpdateSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed += shiftAcceleration;
            if (movementSpeed > shiftSpeed)
            {
                movementSpeed = shiftSpeed;
            } 
        }
        else
        {
            movementSpeed += normalAcceleration;
            if (movementSpeed > normalSpeed)
            {
                movementSpeed = normalSpeed;
            }
        }
    }

    void UpdateMovement()
    {
        float xMovement = 0f;
        float yMovement = 0f;
        float zMovement = 0f;

        bool resetSpeed = false;

        if (Input.GetKey(KeyCode.W))
        {
            zMovement += movementSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zMovement -= movementSpeed;
        }
        else
        {
            resetSpeed = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xMovement -= movementSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xMovement += movementSpeed;
        }
        else if (resetSpeed)
        {
            movementSpeed = 0f;
        }

        xMovement *= Time.deltaTime;
        zMovement *= Time.deltaTime;

        // Move object relative to its position
        MainCamera.transform.Translate(xMovement, yMovement, zMovement, Space.Self);
    }
}
