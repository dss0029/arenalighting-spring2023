using UnityEngine;

public class FreeCameraControl : MonoBehaviour
{
    public Camera MainCamera;

    public float rotationSensitivity = 50f;
    public float rotationX = 0f;
    public float rotationY = 0f;
    public float movementSpeed;

    [SerializeField]
    bool controlEnabled = false;
    
    [SerializeField]
    float normalSpeed, shiftSpeed, normalAcceleration, shiftAcceleration;

    // Update is called once per frame
    void Update()
    {
        if (!controlEnabled) return;

        UpdateSpeed();
        UpdateMovement();

        // Rotate object using a mouse
        rotationX += Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        MainCamera.transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);

    }

    void UpdateCurrentRotation()
    {
        Transform cameraTransform = MainCamera.transform;
        rotationX = cameraTransform.localEulerAngles.y;
        rotationY = cameraTransform.localEulerAngles.x;
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

        if (Input.GetKey(KeyCode.W))
        {
            zMovement += movementSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zMovement -= movementSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xMovement -= movementSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xMovement += movementSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            yMovement += movementSpeed;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            yMovement -= movementSpeed;
        }

        if (xMovement == 0 && yMovement == 0 && zMovement == 0)
        {
            movementSpeed = 0f;
            return;
        }

        xMovement *= Time.deltaTime;
        zMovement *= Time.deltaTime;
        yMovement *= Time.deltaTime;

        // Move object relative to its position
        MainCamera.transform.Translate(xMovement, yMovement, zMovement, Space.Self);
    }

    public void Enable()
    {
        UpdateCurrentRotation();
        controlEnabled = true;
    }

    public void Disable()
    {
        UpdateCurrentRotation();
        controlEnabled = false;
    }
}
