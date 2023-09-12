using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class DriftManager : MonoBehaviour
{
    public Rigidbody player;

    private float speed;
    private float driftAngle = 0;
    private float driftFactor = 1;
    private bool isDrifting = false;
    private int currentScore;
    private IEnumerator stopDriftCoroutine = null;
    private float minSpeedToDrift = 5;
    private float minAngleToDrift = 10;
    private float driftingDelay = 0.2f;

    public CanvasGroup canvasGroup;
    public CanvasGroup totalScoreGroup;
    public float duration = .4f;

    public Wheels wheels;

    public TMP_Text multperValue;
    public TMP_Text currentScoreText;
    public TMP_Text totalScoreText;
    public Image progressCircle;

    private float maxScore = 300;
    private float multiper = 1.0f;
    

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
        StartCoroutine(DoShow(canvasGroup, canvasGroup.alpha, 1));
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
        totalScoreText.text = Mathf.RoundToInt(currentScore * multiper).ToString() + " pts";
        isDrifting = false;
        yield return new WaitForSeconds(0.5f);

        currentScore = 0;
        StartCoroutine(DoHide(canvasGroup, canvasGroup.alpha, 0));
        DriftScoreClear();

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(DoShow(totalScoreGroup, totalScoreGroup.alpha, 1));
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(DoHide(totalScoreGroup, totalScoreGroup.alpha, 0));
    }

    public void DriftScore()
    {
        progressCircle.fillAmount = Mathf.Lerp(0, 1, currentScore / maxScore);
        currentScoreText.text = currentScore.ToString();
        if(currentScore >= maxScore)
        {
            progressCircle.fillAmount = 0f;
            multiper += 0.5f;
            maxScore *= 2;
            multperValue.text = "x"+ multiper;
        }
    }

    public void DriftScoreClear()
    {
        progressCircle.fillAmount = 0f;
        multiper = 1.0f;
        maxScore = 350;
        multperValue.text = "x"+ multiper;
    }

     public IEnumerator DoHide(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }
    public IEnumerator DoShow(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }
}