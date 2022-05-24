using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : BaseMotor
{
    private CameraMotor cameraMotor;
   private Transform camTransform;


    protected override void Start ()
    {
        base.Start ();

        cameraMotor = gameObject.AddComponent<CameraMotor>();
        cameraMotor.Init();
        camTransform = cameraMotor.CameraContainer;

    }

    protected override void UpdateMotor()
    {
        MoveVector = InputDirection();

        //Rotate our MoveVectir with Camera's foward
        MoveVector = RotateWithView(MoveVector);

        //Send the input to a filter
        MoveVector = state.ProcessMotion(MoveVector);
        RotationQuaternion = state.ProcessRotation(MoveVector);

        //Check if we need to change current state
        state.Transition();

        //Move
        Move();
        Rotate();

    }

    private Vector3 InputDirection()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.Z))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        return direction;
    }

    private Vector3 RotateWithView(Vector3 input)
    {
        Vector3 view = camTransform.TransformDirection(input);
        view.Set(view.x, 0, view.z);
        return view.normalized * input.magnitude;
    }
}
