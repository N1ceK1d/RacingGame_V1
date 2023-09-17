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

    public float accelerationInput;
    public float torque;
    public float brakePower;

    void Update()
    {
        accelerationInput = Input.GetAxis("Vertical");
        SetDriveType();
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

    public void Handbrake(float speed)
    {
        if(Input.GetKey(KeyCode.Space))
        {
            wheels.FR.wheelCollider.brakeTorque = brakePower;
            wheels.FL.wheelCollider.brakeTorque = brakePower;
            if(Input.GetKeyDown(KeyCode.Space) && (int)speed > 0)
            {
                wheels.WheelEffectsStart();
            }
        }
        else 
        {
            wheels.FR.wheelCollider.brakeTorque = 0;
            wheels.FL.wheelCollider.brakeTorque = 0;
            if(Input.GetKeyUp(KeyCode.Space) || (int)speed == 0)
            {
                wheels.WheelEffectsStop();
            }
        }
    }

    public void AccelerateFWD()
    {
        wheels.FR.wheelCollider.motorTorque = torque * accelerationInput;
        wheels.FL.wheelCollider.motorTorque = torque * accelerationInput;
    }

    public void AccelerateRWD()
    {
        wheels.RR.wheelCollider.motorTorque = torque * accelerationInput;
        wheels.RL.wheelCollider.motorTorque = torque * accelerationInput;
    }

    public void AccelerateAWD()
    {
        wheels.FR.wheelCollider.motorTorque = torque * accelerationInput;
        wheels.FL.wheelCollider.motorTorque = torque * accelerationInput;
        wheels.RR.wheelCollider.motorTorque = torque * accelerationInput;
        wheels.RL.wheelCollider.motorTorque = torque * accelerationInput;
    }

}
