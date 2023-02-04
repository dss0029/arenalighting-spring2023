using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialView : MonoBehaviour
{
    public Camera MainCamera;
    public float speed;

    private float rotationY;
    private float rotationX;

    private Transform target;

    private float distanceFromTarget;

    // Update is called once per frame
    void Update()
    {
        rotationY += speed * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -40, 40);

        MainCamera.transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);

        MainCamera.transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
