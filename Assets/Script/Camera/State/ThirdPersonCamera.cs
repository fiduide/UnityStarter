using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : BaseCameraState
{
    private const float Y_ANGLE_MIN = -40.0f;
    private const float Y_ANGLE_MAX = 40.0f;

    private Transform lookAt;
    private Transform CameraContainer;

    private Vector3 offset = Vector3.up;
    public float distance = 7.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 1.0f;
    private float sensitivityY = 0.5f;

    public override void Construct ()
    {
        base.Construct ();

        lookAt = transform;
        CameraContainer = motor.CameraContainer;
    }

    public override Vector3 ProcessMotion(Vector3 input)
    {
        currentX += input.x * sensitivityX;
        currentY += input.z * sensitivityY;

        currentY = ClampAngle(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        return CalculatePosition();
    }

    public override Quaternion ProcessRotation(Vector3 input)
    {
        return Quaternion.Euler(currentY, currentX, 0);
    }

    private Vector3 CalculatePosition()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        return (lookAt.position + offset) + rotation * direction;
    }
}
