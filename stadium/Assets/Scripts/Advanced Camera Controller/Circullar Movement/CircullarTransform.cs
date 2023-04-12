using UnityEngine;

[System.Serializable]
public class CircullarTransform: ICameraMovement
{
    public CameraTransformType transformType { get; } = CameraTransformType.Circullar;
    public bool smoothnessEnabled { get; set; } = true;

    public Vector3? targetPosition { get; set; }
    public Vector3 startRotation { get; set; }
    public Vector3 endRotation { get; set; }
    public float startDistanceFromTarget { get; set; }
    public float endDistanceFromTarget { get; set; }
    public float duration { get; set; }

    public CircullarTransform(Vector3? targetPosition, Vector3 startRotation, Vector3 endRotation, float startDistanceFromTarget, float endDistanceFromTarget, float duration)
    {
        this.targetPosition = targetPosition;
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.startDistanceFromTarget = startDistanceFromTarget;
        this.endDistanceFromTarget = endDistanceFromTarget;
        this.duration = duration;
    }
}
