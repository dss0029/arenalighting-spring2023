using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraTransformController : MonoBehaviour
{
    [SerializeField]
    private Camera controlCamera;

    public void StartMovement(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float duration)
    {
        StartCoroutine(TransformCamera(startPosition, endPosition, startRotation, endRotation, duration));
    }

    IEnumerator TransformCamera(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float timeElapsed = 0;
        controlCamera.transform.position = startPosition;
        controlCamera.transform.rotation = startRotation;


        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            Quaternion newRotation = Quaternion.Lerp(startRotation, endRotation, t);

            controlCamera.transform.position = newPosition;
            controlCamera.transform.rotation = newRotation;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        controlCamera.transform.position = endPosition;
        controlCamera.transform.rotation = endRotation;
    }
}
