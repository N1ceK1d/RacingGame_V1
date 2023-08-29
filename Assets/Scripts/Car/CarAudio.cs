using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCar : MonoBehaviour
{
    public AudioSource runningSound;
    public float runningMaxVolume;
    public float runningMaxPitch;

    public AudioSource reverseSound;
    public float reverseMaxVolume;
    public float reverseMaxPitch;

    public AudioSource idleSound;
    public float idleMaxVolume;

    private float revLimiter;

    public float speedRatio;
    public float limiterSound = 0.1f;
    public float limiterFrequency = 3f;
    public float limiterEngage = 2.5f;

    public AudioSource startingSound;
    public AudioSource brakingSound;

    public Car_Controller carController;

    void Update()
    {   
        WorkingEngine();
    }

    public void WorkingEngine()
    {
        float speedSign = 0;
        if(carController)
        {
            speedSign = Mathf.Sign(carController.GetSpeedRatio());
            speedRatio = Mathf.Abs(carController.GetSpeedRatio());
        }
        if(speedRatio > limiterEngage)
        {
            revLimiter = (Mathf.Sin(Time.time * limiterFrequency) + 1f) * limiterSound * (speedRatio - limiterEngage);
        }

        
            idleSound.volume = Mathf.Lerp(0.1f, idleMaxVolume, speedRatio);
            if(speedSign > 0)
            {
                reverseSound.volume = 0;
                runningSound.volume = Mathf.Lerp(0.3f, runningMaxVolume, speedRatio);
                runningSound.pitch = Mathf.Lerp(runningSound.pitch , Mathf.Lerp(0.3f, runningMaxPitch, speedRatio) + revLimiter, Time.deltaTime);
            }
            else 
            {
                runningSound.volume = 0;
                reverseSound.volume = Mathf.Lerp(0f, reverseMaxVolume, speedRatio);
                reverseSound.pitch = Mathf.Lerp(reverseSound.pitch, Mathf.Lerp(0.2f, reverseMaxPitch, speedRatio) + revLimiter, Time.deltaTime);
            }
    }

    public void StarEngine()
    {
        startingSound.Play();
    }

    public void StopEngine()
    {
        brakingSound.Play();
    }
}
