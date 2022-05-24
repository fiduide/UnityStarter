using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplySpeed(ref input, motor.Speed);
        ApplyGravity(ref input, motor.Gravity);
        
        return input;
    }

      public override void Transition ()
    {
        if(motor.Grounded())
            motor.ChangeState("WalkingState");
    }
}
