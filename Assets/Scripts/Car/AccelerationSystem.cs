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
    public float enginePower;
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
            wheels.RR.wheelCollider.brakeTorque = brakePower;
            wheels.RL.wheelCollider.brakeTorque = brakePower;
            if(Input.GetKeyDown(KeyCode.Space) && (int)speed > 0)
            {
                wheels.WheelEffectsStart();
            }
        }
        else 
        {
            wheels.RR.wheelCollider.brakeTorque = 0;
            wheels.RL.wheelCollider.brakeTorque = 0;
            if(Input.GetKeyUp(KeyCode.Space) || (int)speed == 0)
            {
                wheels.WheelEffectsStop();
            }
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
