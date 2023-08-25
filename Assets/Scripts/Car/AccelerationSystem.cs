using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationSystem : MonoBehaviour
{
    public enum DriveType 
    {
        FWD,
        RWD,
        AWD
    }

    
    public WheelCollider FR;
    public WheelCollider FL;
    public WheelCollider RR;
    public WheelCollider RL;
    

    public DriveType driveType;

    private float accelerationInput;
    public float enginePower;
    public float brakePower;

    void Update()
    {
        accelerationInput = Input.GetAxis("Vertical");
        SetDriveType();
        Handbrake();
    }

    public void SetDriveType()
    {
        switch(driveType)
        {
            case DriveType.FWD:
                AccelerateFWD();
                break;
            case DriveType.RWD:
                AccelerateRWD();
                break;
            case DriveType.AWD:
                AccelerateAWD();
                break;
            default:
                AccelerateRWD();
                driveType = DriveType.RWD;
                break;
        }
    }

    public void Handbrake()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            RR.brakeTorque = brakePower;
            RL.brakeTorque = brakePower;
        }
        else 
        {
            RR.brakeTorque = 0;
            RL.brakeTorque = 0;
        }
        
    }

    public void AccelerateFWD()
    {
        FR.motorTorque = enginePower * accelerationInput;
        FL.motorTorque = enginePower * accelerationInput;
    }

    public void AccelerateRWD()
    {
        RR.motorTorque = enginePower * accelerationInput;
        RL.motorTorque = enginePower * accelerationInput;
    }

    public void AccelerateAWD()
    {
        FR.motorTorque = enginePower * accelerationInput;
        FL.motorTorque = enginePower * accelerationInput;
        RR.motorTorque = enginePower * accelerationInput;
        RL.motorTorque = enginePower * accelerationInput;
    }

}
