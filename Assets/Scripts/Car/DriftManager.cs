using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro.EditorUtilities;
using TMPro;

public class DriftManager : MonoBehaviour
{
    public Rigidbody player;

    private float speed;
    private float driftAngle = 0;
    private float driftFactor = 1;
    private bool isDrifting = false;
    private int currentScore;
    private int totalScore;
    private IEnumerator stopDriftCoroutine = null;
    private float minSpeedToDrift = 5;
    private float minAngleToDrift = 10;
    private float driftingDelay = 0.2f;

    public Wheels wheels;

    public TMP_Text multperValue;
    public TMP_Text currentScoreText;
    public Image progressCircle;

    private float maxScore = 300;
    private float minScore = 0;
    private float multiper = 0.0f;
    

    private void Update()
    {
        ManagerDrift();
        DriftScore();
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
           currentScore += Mathf.RoundToInt(Time.deltaTime * driftAngle * driftFactor);
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

    public void DriftScore()
    {
        progressCircle.fillAmount = Mathf.Lerp(0, 1, currentScore / maxScore);
        currentScoreText.text = currentScore.ToString();
        if(currentScore >= maxScore)
        {
            progressCircle.fillAmount = 0f;
            multiper += 0.5f;
            multperValue.text = "x"+multiper;
            currentScore = 0;
            Debug.Log("Maxxx");
        }
    }


}