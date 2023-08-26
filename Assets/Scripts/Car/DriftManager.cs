using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DriftManager : MonoBehaviour
{
    public Rigidbody player;

    private float speed;
    private float driftAngle = 0;
    private float driftFactor = 1;
    private bool isDrifting = false;
    private float currentScore;
    private float totalScore;
    private IEnumerator stopDriftCoroutine = null;
    private float minSpeedToDrift = 5;
    private float minAngleToDrift = 10;
    private float driftingDelay = 0.2f;

    public Wheels wheels;

    private void Update()
    {
        ManagerDrift();
    }

    private void ManagerDrift()
    {
        speed = player.velocity.magnitude;
        driftAngle = Vector3.Angle(player.transform.forward, (player.velocity + player.transform.forward).normalized);
        if(driftAngle > 120)
        {
            driftAngle = 0;
        }
        if(driftAngle >= minAngleToDrift && speed > minSpeedToDrift)
        {
            if(!isDrifting || stopDriftCoroutine != null)
            {
                StartDrift();
                wheels.WheelEffectsStart();
            }
        }
        else 
        {
            if(isDrifting && stopDriftCoroutine == null)
            {
                StopDrift();
                wheels.WheelEffectsStop();
            }
        }
        if(isDrifting)
        {
           currentScore += Time.deltaTime * driftAngle * driftFactor;
           driftFactor += Time.deltaTime;
        }
    }

    private async void StartDrift()
    {
        if(!isDrifting)
        {
            await Task.Delay(Mathf.RoundToInt(100 * driftingDelay));
            driftFactor = 1;
        }
        if(stopDriftCoroutine != null)
        {
            StopCoroutine(stopDriftCoroutine);
            stopDriftCoroutine = null;
        }
        isDrifting = true;
    }

    private void StopDrift()
    {
        stopDriftCoroutine = StoppingDrift();
        StartCoroutine(stopDriftCoroutine);
    }

    private IEnumerator StoppingDrift() 
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(driftingDelay * 4f);
        totalScore += currentScore;
        isDrifting = false;
        yield return new WaitForSeconds(0.5f);
        currentScore = 0;
    }

}