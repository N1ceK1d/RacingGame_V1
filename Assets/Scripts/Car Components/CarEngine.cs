using UnityEngine;

public class CarEngine : MonoBehaviour
{
    [Header("Engine parameters")]
    public float enginePower;
    public float maxRPM;
    public float maxEngineSpeed;
    public float maxSpeed;
    public bool isStarted;

    public EngineAudio engineAudio;

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
        if(target.velocity.magnitude > maxEngineSpeed / 3.6f)
        {
            target.velocity = Vector3.ClampMagnitude(target.velocity, maxEngineSpeed / 3.6f);
        }
    }

    public float GetRPM(float currentRPM)
    {
        float rpm;
        rpm = Mathf.Clamp(0, maxRPM, currentRPM * 10000);
        return rpm;
    }
    public void EngineOnOff()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            isStarted = !isStarted;
            if(isStarted)
            {
                engineAudio.StarEngine();
            }
            else
            {
                engineAudio.StopEngine();
            }
        }
    }
}