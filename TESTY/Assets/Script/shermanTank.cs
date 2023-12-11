using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shermanTank : MonoBehaviour
{
    public float maxSpeed = 200f;//Replace with your max speed
    [SerializeField] private GameObject turret, aim_point;
    [SerializeField] private GameObject Ltrack, Rtrack;
    [SerializeField] private GameObject Lwheel, Rwheel;
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;

    private Rigidbody rigTank;
    private Renderer rendL, rendR;
    private int iDir;
    private float fLturn, fRturn;

    // Settings
    [SerializeField] private float motorForce, maxSteerAngle;//, breakForce

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    void Start()
    {
        rigTank = GetComponent<Rigidbody>();
        rendL = Ltrack.GetComponent<Renderer>();
        rendR = Rtrack.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();

        //Debug.Log("Y:" + Input.mousePosition.x);
        // Vector3 turDirection = Input.mousePosition - turret.position;
        // Quaternion rotation = Quaternion.LookRotation(direction);
        // transform.rotation = rotation;

        if (rigTank.velocity.magnitude > maxSpeed)
        {
            rigTank.velocity = rigTank.velocity.normalized * maxSpeed;
        }

        //float offset = Time.time * speed * -1;
        if (rigTank.velocity.magnitude > maxSpeed)
        {
            rigTank.velocity = rigTank.velocity.normalized * maxSpeed;
        }

        float speed = rigTank.velocity.magnitude/1;
        if (speed > 0.02f) { speed = 0.02f; }
        float offset = Time.time * speed * iDir;
        rendL.material.SetTextureOffset("_MainTex", new Vector2(offset * fLturn, 0f));
        rendR.material.SetTextureOffset("_MainTex", new Vector2(offset * fRturn, 0f));

        for (int i = 0; i < Lwheel.transform.childCount; i++)
        {
            // Calculate rotation based on wheel diameter and speed value
            float wheelSize = Lwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
            float rotationAngle = offset / wheelSize;

            //Apply rotation
            Lwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
        }

        for (int i = 0; i < Rwheel.transform.childCount; i++)
        {
            // Calculate rotation based on wheel diameter and speed value
            float wheelSize = Rwheel.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds.size.y;
            float rotationAngle = offset / wheelSize;

            //Apply rotation
            Rwheel.transform.GetChild(i).transform.Rotate(rotationAngle, 0, 0, Space.Self);
        }
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput> 0) 
        {
            fLturn = horizontalInput; 
        } 
        else if (horizontalInput < 0)
        {
            fRturn = horizontalInput * -1; 
        }
        else
        {
            fLturn = 1.0f;
            fRturn = 1.0f;
        }

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput >=0) { iDir = -1; } else { iDir = 1;}

    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }



}