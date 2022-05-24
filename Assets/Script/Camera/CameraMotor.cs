using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
   private BaseCameraState state;

   public Transform CameraContainer{set;get;}
   public Vector3 InputVector{set;get;}

   public void Init()
    {
        CameraContainer = new GameObject("Camera Container").transform;
        CameraContainer.gameObject.AddComponent<Camera>();

        state = gameObject.AddComponent<ThirdPersonCamera>() as BaseCameraState;
        state.Construct();
    }

    private void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal2");
        direction.z = Input.GetAxis("Vertical2");
        if(direction.magnitude > 1)
        {
            direction.Normalize();
        }

        InputVector = direction;
    }

    private void LateUpdate()
    {
        CameraContainer.position = state.ProcessMotion(InputVector);
        CameraContainer.rotation = state.ProcessRotation(InputVector);
    }

    public void ChangeState(string stateName)
    {
        System.Type type = System.Type.GetType(stateName);

        state.Destruct();
        state = gameObject.AddComponent(type) as BaseCameraState;
        state.Construct();
    }
}
