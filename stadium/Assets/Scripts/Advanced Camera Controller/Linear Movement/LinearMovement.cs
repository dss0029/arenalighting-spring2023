using System.Threading.Tasks;
using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    public async Task TransformCamera(LinearTransform linearTransform)
    {
        if (linearTransform.smoothnessEnabled) {
            await SmoothTransformCamera(linearTransform.startPosition ?? mainCamera.transform.position,
                                        linearTransform.endPosition,
                                        linearTransform.startRotation ?? mainCamera.transform.rotation.eulerAngles,
                                        linearTransform.endRotation,
                                        linearTransform.duration);
        }
        else
        {
            await BasicTransformCamera(linearTransform.startPosition ?? mainCamera.transform.position,
                                       linearTransform.endPosition,
                                       linearTransform.startRotation ?? mainCamera.transform.rotation.eulerAngles,
                                       linearTransform.endRotation,
                                       linearTransform.duration);
        }
    }

    async Task BasicTransformCamera(Vector3 startPosition, Vector3 endPosition, Vector3 startRotation, Vector3 endRotation, float duration)
    {
        float timeElapsed = 0;
        mainCamera.transform.position = startPosition;
        mainCamera.transform.rotation = Quaternion.Euler(startRotation);


        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            Quaternion newRotation = Quaternion.Lerp(Quaternion.Euler(startRotation), Quaternion.Euler(endRotation), t);

            mainCamera.transform.position = newPosition;
            mainCamera.transform.rotation = newRotation;

            timeElapsed += Time.deltaTime;

            await Task.Yield();
        }

        mainCamera.transform.position = endPosition;
        mainCamera.transform.rotation = Quaternion.Euler(endRotation);
    }

    async Task SmoothTransformCamera(Vector3 startPosition, Vector3 endPosition, Vector3 startRotation, Vector3 endRotation, float duration)
    {
        float timeElapsed = 0;
        mainCamera.transform.position = startPosition;
        mainCamera.transform.rotation = Quaternion.Euler(startRotation);


        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            Quaternion newRotation = Quaternion.Lerp(Quaternion.Euler(startRotation), Quaternion.Euler(endRotation), t);

            mainCamera.transform.position = newPosition;
            mainCamera.transform.rotation = newRotation;

            timeElapsed += Time.deltaTime;

            await Task.Yield();
        }

        mainCamera.transform.position = endPosition;
        mainCamera.transform.rotation = Quaternion.Euler(endRotation);
    }
}
