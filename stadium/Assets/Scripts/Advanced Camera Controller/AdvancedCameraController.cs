using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


class CameraTransform
{
    public Vector3? startPosition { get; set; }
    public Vector3 endPosition { get; set; }
    public Quaternion? startRotation { get; set; }
    public Quaternion endRotation { get; set; }
    public float duration;

    public CameraTransform(Vector3? startPosition, Vector3 endPosition, Quaternion? startRotation, Quaternion endRotation, float duration)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.duration = duration;
    }

}

public class AdvancedCameraController : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    AerialView aerialView;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = new Vector3(-27.5f, 38.5f, -52.5f);
        Vector3 endPosition = new Vector3(24.0f, 16.4f, 27.2f);

        Vector3 startRotation = new Vector3(41.7f, 33.4f, 0.0f);
        Vector3 endRotation = new Vector3(17.2f, 224.6f, 0.0f);
        //StartCoroutine(TransformCamera(startPosition, endPosition, Quaternion.Euler(startRotation), Quaternion.Euler(endRotation), 5));

        List<CameraTransform> cameraTransforms = new List<CameraTransform>();
        //cameraTransforms.Add(new CameraTransform(new Vector3(), new Vector3(), Quaternion.Euler(new Vector3()), Quaternion.Euler(new Vector3()), 5));

        cameraTransforms.Add(new CameraTransform(new Vector3(332.3f, 469.0f, -412.8f), new Vector3(332.3f, 469.0f, -412.8f), Quaternion.Euler(new Vector3(38.5f, 320.6f, 0.0f)), Quaternion.Euler(new Vector3(38.5f, 320.6f, 0.0f)), 3));
        cameraTransforms.Add(new CameraTransform(new Vector3(332.3f, 469.0f, -412.8f), new Vector3(37.5f, 55.6f, -51.9f), Quaternion.Euler(new Vector3(38.5f, 320.6f, 0.0f)), Quaternion.Euler(new Vector3(40.2f, 315.1f, 0.0f)), 3));
        cameraTransforms.Add(new CameraTransform(null, new Vector3(23.4f, 81.4f, 47.3f), null, Quaternion.Euler(new Vector3(50.0f, 204.3f, 0.0f)), 5));
        cameraTransforms.Add(new CameraTransform(null, new Vector3(24.0f, 16.4f, 27.2f), null, Quaternion.Euler(new Vector3(17.2f, 224.6f, 0.0f)), 5));
        cameraTransforms.Add(new CameraTransform(null, new Vector3(-2.1f, 4.9f, -28.8f), null, Quaternion.Euler(new Vector3(7.6f, 324.2f, 0.0f)), 5));
        cameraTransforms.Add(new CameraTransform(null, new Vector3(-34.6f, 46.5f, 36.7f), null, Quaternion.Euler(new Vector3(40.7f, 136.6f, 0.0f)), 5));
        cameraTransforms.Add(new CameraTransform(null, new Vector3(-7.8f, 18.3f, -24.8f), null, Quaternion.Euler(new Vector3(37.2f, 33.1f, 0.0f)), 5));
        cameraTransforms.Add(new CameraTransform(null, new Vector3(-46.2f, 71.9f, -83.9f), null, Quaternion.Euler(new Vector3(37.2f, 33.1f, 0.0f)), 5));
        cameraTransforms.Add(new CameraTransform(null, new Vector3(94.8f, 122.9f, 101.9f), null, Quaternion.Euler(new Vector3(38.0f, 216.7f, 0.0f)), 5));
        cameraTransforms.Add(new CameraTransform(null, new Vector3(12.8f, 15.7f, -8.1f), null, Quaternion.Euler(new Vector3(38.0f, 216.7f, 0.0f)), 3));

        StartCameraTransformSequence(cameraTransforms);
    }


    async void StartCameraTransformSequence(List<CameraTransform> cameraTransforms)
    {
        for (int i = 0; i < cameraTransforms.Count; i++)
        {

            var startPos = cameraTransforms[i].startPosition ?? mainCamera.transform.position;
            var startRot = cameraTransforms[i].startRotation ?? mainCamera.transform.rotation;
            var endPos = cameraTransforms[i].endPosition;
            var endRot = cameraTransforms[i].endRotation;


            await SmoothTransformCamera(startPos, endPos, startRot, endRot, cameraTransforms[i].duration);
        }

        Vector3 startAngle = new Vector3(10, 90, 0);
        Vector3 endAngle = new Vector3(80, 270, 0);
        await aerialView.RotateCamera(40, 70, startAngle, endAngle, 20);
    }


    IEnumerator TransformCamera(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float timeElapsed = 0;
        mainCamera.transform.position = startPosition;
        mainCamera.transform.rotation = startRotation;


        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            Quaternion newRotation = Quaternion.Lerp(startRotation, endRotation, t);

            mainCamera.transform.position = newPosition;
            mainCamera.transform.rotation = newRotation;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.position = endPosition;
        mainCamera.transform.rotation = endRotation;
    }

    async Task SmoothTransformCamera(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float timeElapsed = 0;
        mainCamera.transform.position = startPosition;
        mainCamera.transform.rotation = startRotation;


        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            Quaternion newRotation = Quaternion.Lerp(startRotation, endRotation, t);

            mainCamera.transform.position = newPosition;
            mainCamera.transform.rotation = newRotation;

            timeElapsed += Time.deltaTime;

            await Task.Yield();
        }

        mainCamera.transform.position = endPosition;
        mainCamera.transform.rotation = endRotation;
    }
}
