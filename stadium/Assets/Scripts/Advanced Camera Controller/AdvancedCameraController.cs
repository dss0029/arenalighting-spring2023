using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum CameraTransformType
{
    Linear,
    Circullar,
}

public interface ICameraMovement
{
    public CameraTransformType transformType { get; }
    public bool smoothnessEnabled { get; set; }
}

public class AdvancedCameraController : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    LinearMovement linearMovement;

    [SerializeField]
    CircullarMovement circullarMovement;

    // Start is called before the first frame update
    void Start()
    {

        List<ICameraMovement> cameraTransforms = new List<ICameraMovement>();
        //cameraTransforms.Add(new CameraTransform(new Vector3(), new Vector3(), Quaternion.Euler(new Vector3()), Quaternion.Euler(new Vector3()), 5));

        Vector3 startAngle = new Vector3(10, 90, 0);
        Vector3 endAngle = new Vector3(80, 270, 0);
        var cirucllarTransform = new CircullarTransform(null, startAngle, endAngle, 40, 70, 20);

        cameraTransforms.Add(new LinearTransform(new Vector3(332.3f, 469.0f, -412.8f), new Vector3(332.3f, 469.0f, -412.8f), new Vector3(38.5f, 320.6f, 0.0f), new Vector3(38.5f, 320.6f, 0.0f), 3));
        cameraTransforms.Add(new LinearTransform(new Vector3(332.3f, 469.0f, -412.8f), new Vector3(37.5f, 55.6f, -51.9f), new Vector3(38.5f, 320.6f, 0.0f), new Vector3(40.2f, 315.1f, 0.0f), 3));
        cameraTransforms.Add(new LinearTransform(null, new Vector3(23.4f, 81.4f, 47.3f), null, new Vector3(50.0f, 204.3f, 0.0f), 5));
        cameraTransforms.Add(new LinearTransform(null, new Vector3(24.0f, 16.4f, 27.2f), null, new Vector3(17.2f, 224.6f, 0.0f), 5));
        cameraTransforms.Add(new LinearTransform(null, new Vector3(-2.1f, 4.9f, -28.8f), null, new Vector3(7.6f, 324.2f, 0.0f), 5));
        cameraTransforms.Add(cirucllarTransform);
        cameraTransforms.Add(new LinearTransform(null, new Vector3(-34.6f, 46.5f, 36.7f), null, new Vector3(40.7f, 136.6f, 0.0f), 5));
        cameraTransforms.Add(new LinearTransform(null, new Vector3(-7.8f, 18.3f, -24.8f), null, new Vector3(37.2f, 33.1f, 0.0f), 5));
        cameraTransforms.Add(new LinearTransform(null, new Vector3(-46.2f, 71.9f, -83.9f), null, new Vector3(37.2f, 33.1f, 0.0f), 5));
        cameraTransforms.Add(new LinearTransform(null, new Vector3(94.8f, 122.9f, 101.9f), null, new Vector3(38.0f, 216.7f, 0.0f), 5));
        cameraTransforms.Add(new LinearTransform(null, new Vector3(12.8f, 15.7f, -8.1f), null, new Vector3(38.0f, 216.7f, 0.0f), 3));

        StartCameraTransformSequence(cameraTransforms);
    }


    async void StartCameraTransformSequence(List<ICameraMovement> cameraTransforms)
    {
        for (int i = 0; i < cameraTransforms.Count; i++)
        {
            if (cameraTransforms[i] is LinearTransform)
            {
                await linearMovement.TransformCamera(cameraTransforms[i] as LinearTransform);
            }
            else if (cameraTransforms[i] is CircullarTransform)
            {
                await circullarMovement.TransformCamera(cameraTransforms[i] as CircullarTransform);
            }
        }
    }
}
