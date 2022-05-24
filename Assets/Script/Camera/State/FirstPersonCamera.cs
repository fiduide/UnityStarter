using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : BaseCameraState
{
    private const float Y_ANGLE_MIN = -40.0f;
    private const float Y_ANGLE_MAX = 40.0f;

    private float offset = 1.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 1.0f;
    private float sensitivityY = 0.5f;

    public override Vector3 ProcessMotion(Vector3 input)
    {
        return transform.position + (transform.up * offset);
    }

    public override Quaternion ProcessRotation(Vector3 input)
    {

        currentX += input.x * sensitivityX;
        currentY += input.z * sensitivityY;

        currentY = ClampAngle(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        return Quaternion.Euler(currentY, currentX, 0);
    }
}
