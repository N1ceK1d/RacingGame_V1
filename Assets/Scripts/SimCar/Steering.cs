using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public enum SteeringType 
    {
        pozitiveAckerman,
        zeroAckerman,
        negativeAckerman
    };

    public SteeringType steeringType;

    private float ackermanAngleLeft;
    private float ackermanAngleRight;

    private float steeringInput;

    public float wheelBase;
    public float turnRadius;
    public float rearTrack;

    public CarSuspension FR;
    public CarSuspension FL;
    
    
    void Update()
    {
        steeringInput = Input.GetAxis("Horizontal");
        SetSteeringType();
        FR.steerAngle = ackermanAngleRight;
        FL.steerAngle = ackermanAngleLeft;
    }

    public void SetSteering(float steerValue)
    {
        steeringInput = steerValue;
    }

    public void SetSteeringType()
    {   
        switch(steeringType)
        {
            case SteeringType.pozitiveAckerman:
                    PozitiveAckermanSteering();
                break;
            case SteeringType.zeroAckerman:
                    ZeroAckermanSteering();
                break;
            case SteeringType.negativeAckerman:
                    NegativeAckermanSteering();
                break;
            default:
                steeringType = SteeringType.pozitiveAckerman;
                PozitiveAckermanSteering();
                break;
        }
    }

    public void PozitiveAckermanSteering()
    {
        if(steeringInput > 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steeringInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steeringInput;
        }
        else if(steeringInput < 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steeringInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steeringInput;
        }
        else
        {
            ackermanAngleLeft = 0;
            ackermanAngleRight = 0;
        }
    }

    public void ZeroAckermanSteering()
    {
       if(steeringInput > 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steeringInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steeringInput;
        }
        else if(steeringInput < 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steeringInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steeringInput;
        }
        else
        {
            ackermanAngleLeft = 0;
            ackermanAngleRight = 0;
        }
    }

    public void NegativeAckermanSteering()
    {
        if(steeringInput > 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steeringInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steeringInput;
        }
        else if(steeringInput < 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steeringInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steeringInput;
        }
        else
        {
            ackermanAngleLeft = 0;
            ackermanAngleRight = 0;
        }
    }
}
