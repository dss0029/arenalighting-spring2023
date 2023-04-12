using UnityEngine;

[System.Serializable]
public class LinearTransform : ICameraMovement
{
    public CameraTransformType transformType { get; } = CameraTransformType.Linear;
    public bool smoothnessEnabled { get; set; } = true;

    public Vector3? startPosition { get; set; }
    public Vector3 endPosition { get; set; }
    public Vector3? startRotation { get; set; }
    public Vector3 endRotation { get; set; }
    public float duration { get; set; }

    public LinearTransform(Vector3? startPosition, Vector3 endPosition, Vector3? startRotation, Vector3 endRotation, float duration)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.duration = duration;
    }
}
