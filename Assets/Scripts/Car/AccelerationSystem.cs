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

    
    public Wheels wheels;
    
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
            wheels.RR.wheelCollider.brakeTorque = brakePower;
            wheels.RL.wheelCollider.brakeTorque = brakePower;
        }
        else 
        {
            wheels.RR.wheelCollider.brakeTorque = 0;
            wheels.RL.wheelCollider.brakeTorque = 0;
        }
        
    }

    public void AccelerateFWD()
    {
        wheels.FR.wheelCollider.motorTorque = enginePower * accelerationInput;
        wheels.FL.wheelCollider.motorTorque = enginePower * accelerationInput;
    }

    public void AccelerateRWD()
    {
        wheels.RR.wheelCollider.motorTorque = enginePower * accelerationInput;
        wheels.RL.wheelCollider.motorTorque = enginePower * accelerationInput;
    }

    public void AccelerateAWD()
    {
        wheels.FR.wheelCollider.motorTorque = enginePower * accelerationInput;
        wheels.FL.wheelCollider.motorTorque = enginePower * accelerationInput;
        wheels.RR.wheelCollider.motorTorque = enginePower * accelerationInput;
        wheels.RL.wheelCollider.motorTorque = enginePower * accelerationInput;
    }

}
