using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [Header("Engine parameters")]
    public float enginePower; //В Киловаттах
    public float maxRPM;
    public float maxEngineSpeed;
    public float maxSpeed;
    public bool isStarted;
    public float engineTorque;

    public float MaxSpeed(float wheelCirle, float mainGearRatio, float gearRatio)
    {
        maxSpeed = (wheelCirle / (mainGearRatio * gearRatio)) * (60f/1000f) * maxRPM;
        return maxSpeed;
    }

    public void LimitSpeed(Rigidbody target)
    {
        if(target.velocity.magnitude > maxSpeed / 3.6f)
        {
            target.velocity = Vector3.ClampMagnitude(target.velocity, maxSpeed / 3.6f);
        }
    }

    public float CalculateWheelTorque(float engineTorque, float currentGearRatio, float mainGearRatio)
    {
        float torque = 0.0f;
        torque = (engineTorque * currentGearRatio * mainGearRatio) / 2f;
        return torque;
    }

    public float CalculateEngineTorque()
    {
        engineTorque = enginePower * 9550f / maxRPM; 
        return engineTorque;
    }
}