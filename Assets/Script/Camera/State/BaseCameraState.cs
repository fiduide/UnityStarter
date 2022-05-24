using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCameraState : MonoBehaviour
{
    protected CameraMotor motor;

    public virtual void Construct()
    {
         motor = GetComponent<CameraMotor>();
    }

    public virtual void Destruct()
    {
        Destroy(this);
    }

    public virtual void Transition()
    {

    }


   public abstract Vector3 ProcessMotion(Vector3 input);
   public virtual Quaternion ProcessRotation(Vector3 input)
   {
       return transform.rotation;
   }

   protected float ClampAngle(float angle, float min, float max)
   {
       do {
           if (angle < -360)
               angle += 360;
           if (angle > 360)
               angle -= 360;
       } while (angle < -360 || angle > 360);

       return Mathf.Clamp(angle, min, max);
   }
}
