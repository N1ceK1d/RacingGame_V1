using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [Header("Engine parameters")]
    public float enginePower;
    public float maxRPM;
    public float maxEngineSpeed;
    public float maxSpeed;
    public bool isStarted;

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
}