using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    public override void Construct()
    {
        base.Construct();

        motor.VecticalVelocity = motor.JumpForce;
    }

    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplySpeed(ref input, motor.Speed);
        ApplyGravity(ref input, motor.Gravity);

        return input;
    }

    public override void Transition()
    {
        if (motor.VecticalVelocity < 0.0f)
            motor.ChangeState("FallingState");
    }

}
