using System.Threading.Tasks;
using UnityEngine;

public class AerialView : MonoBehaviour {

    public Camera mainCamera;

    [SerializeField]
    public float inclineAngle = 30;
    public float zAngle = 30;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget;


    async public Task RotateCamera(float startDistance, float endDistance, Vector3 startAngle, Vector3 endAngle, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);

            float currentDistance = Mathf.Lerp(startDistance, endDistance, t);
            Vector3 currentAngle = Vector3.Lerp(startAngle, endAngle, t);

            mainCamera.transform.position = target.position - mainCamera.transform.forward * currentDistance;
            mainCamera.transform.localEulerAngles = currentAngle;

            //new Vector3(inclineAngle, currentAngle, zAngle);

            timeElapsed += Time.deltaTime;

            await Task.Yield();
        }

        mainCamera.transform.position = target.position - mainCamera.transform.forward * endDistance;
        mainCamera.transform.localEulerAngles = endAngle;
    }
}
