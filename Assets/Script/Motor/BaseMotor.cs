using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMotor : MonoBehaviour
{
    protected CharacterController controller;
    protected BaseState state;
    protected Transform thisTransform;

    private float baseSpeed = 5.0f;	
    private float baseGravity = 25.0f;
    private float baseJumpForce = 7.0f;
    private float terminalVelocity = 30.0f;
    private float groundedRayDistance = 0.5f;
    private float groundRayInnerOffset = 0.1f;

    public float Speed{get{return baseSpeed;}}
    public float Gravity{get{return baseGravity;}}
    public float JumpForce{get{return baseJumpForce;}}
    public float TerminalVelocity{get{return terminalVelocity;}}

    public float VecticalVelocity{set;get;}
    public Vector3 MoveVector{set;get;}
    public Quaternion RotationQuaternion{set;get;}

    protected abstract void UpdateMotor();

    protected virtual void Start() 
    {
        controller = gameObject.AddComponent<CharacterController>();
        thisTransform = transform;

        state = gameObject.AddComponent<WalkingState>();
        state.Construct();
    }

    private void Update() 
    {
        UpdateMotor();
    }

    protected virtual void Move(){
        controller.Move(MoveVector * Time.deltaTime);
    }

    protected virtual void Rotate(){
        thisTransform.rotation = RotationQuaternion;
    }

    public void ChangeState(string stateName)
    {
        System.Type t = System.Type.GetType(stateName);
        state.Destruct();
        state = gameObject.AddComponent(t) as BaseState;
        state.Construct();
    }

    public virtual bool Grounded()
    {
        RaycastHit hit;
        Vector3 ray;

        float yRay = (controller.bounds.center.y - controller.bounds.extents.y) + 0.3f, 
        centerX = controller.bounds.center.x,
        centerZ = controller.bounds.center.z,
        extentX = controller.bounds.extents.x - groundRayInnerOffset,
        extentZ = controller.bounds.extents.z - groundRayInnerOffset;

        //Middle Raycast
        ray = new Vector3(centerX, yRay, centerZ);
        Debug.DrawRay(ray, Vector3.down, Color.red);

        if(Physics.Raycast(ray, Vector3.down, out hit, groundedRayDistance))
        {
            return true;
        }

    
        ray = new Vector3(centerX + extentX, yRay, centerZ + extentZ);
        Debug.DrawRay(ray, Vector3.down, Color.red);

        if(Physics.Raycast(ray, Vector3.down, out hit, groundedRayDistance))
        {
            return true;
        }

        
        ray = new Vector3(centerX - extentX, yRay, centerZ + extentZ);
        Debug.DrawRay(ray, Vector3.down, Color.red);

        if(Physics.Raycast(ray, Vector3.down, out hit, groundedRayDistance))
        {
            return true;
        }

        
        ray = new Vector3(centerX - extentX, yRay, centerZ - extentZ);
        Debug.DrawRay(ray, Vector3.down, Color.red);

        if(Physics.Raycast(ray, Vector3.down, out hit, groundedRayDistance))
        {
            return true;
        }

        ray = new Vector3(centerX + extentX, yRay, centerZ - extentZ);
        Debug.DrawRay(ray, Vector3.down, Color.red);

        if(Physics.Raycast(ray, Vector3.down, out hit, groundedRayDistance))
        {
            return true;
        }
        return false;
    }


}
