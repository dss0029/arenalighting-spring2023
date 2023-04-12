using System.Threading.Tasks;
using UnityEngine;

public class CircullarMovement : MonoBehaviour {

    public Camera mainCamera;

    [SerializeField]
    private float inclineAngle = 30;

    [SerializeField]
    private float zAngle = 30;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget;


    public async Task TransformCamera(CircullarTransform circullarTransform)
    {
        await RotateCamera(circullarTransform.targetPosition ?? Vector3.zero,
                           circullarTransform.startDistanceFromTarget,
                           circullarTransform.endDistanceFromTarget,
                           circullarTransform.startRotation,
                           circullarTransform.endRotation,
                           circullarTransform.duration);
    }


    async Task RotateCamera(Vector3 targetPosition, float startDistance, float endDistance, Vector3 startAngle, Vector3 endAngle, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);

            float currentDistance = Mathf.Lerp(startDistance, endDistance, t);
            Vector3 currentAngle = Vector3.Lerp(startAngle, endAngle, t);

            mainCamera.transform.position = targetPosition - mainCamera.transform.forward * currentDistance;
            mainCamera.transform.localEulerAngles = currentAngle;

            //new Vector3(inclineAngle, currentAngle, zAngle);

            timeElapsed += Time.deltaTime;

            await Task.Yield();
        }

        mainCamera.transform.position = target.position - mainCamera.transform.forward * endDistance;
        mainCamera.transform.localEulerAngles = endAngle;
    }
}
