using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 startRotation;
    public float speed = 2.0f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = startPosition;
        targetRotation = Quaternion.Euler(startRotation);
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    public void MoveTo(Vector3 pos, Vector3 rotationVector)
    {
        targetPosition = pos;
        targetRotation = Quaternion.Euler(rotationVector);
    }
}
