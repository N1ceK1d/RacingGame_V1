using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public float enginePower;
    public float maxRPM;
    public float maxEngineSpeed;
    public float maxSpeed;
    public float currentSpeed;
    public bool isStarted;


    public float MaxSpeed(float wheelCirle, float mainGearRatio, float gearRatio)
    {
        maxSpeed = (wheelCirle / (mainGearRatio * gearRatio)) * (60f/1000f) * maxRPM;
        return maxSpeed;
    }

    public float MinSpeed()
    {
        return 0f;
    }

    public void LimitSpeed(Rigidbody target)
    {
        if(target.velocity.magnitude > maxSpeed / 3.6f)
        {
            target.velocity = Vector3.ClampMagnitude(target.velocity, maxSpeed / 3.6f);
        }
        if(target.velocity.magnitude > maxEngineSpeed / 3.6f)
        {
            target.velocity = Vector3.ClampMagnitude(target.velocity, maxEngineSpeed / 3.6f);
        }
    }

    public void CurrentSpeed(Rigidbody target)
    {
        currentSpeed = target.velocity.magnitude * 3.6f;
    }


    public float GetRPM(float currentRPM)
    {
        float rpm;
        rpm = Mathf.Clamp(0, maxRPM, currentRPM * 10000);
        return rpm;
    }
}
