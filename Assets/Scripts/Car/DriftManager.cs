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

    public float minSpeedToDrift = 5;
    public float minAngleToDrift = 10;
    public float driftingDelay = 0.2f;


    private IEnumerator stopDriftCoroutine = null;

    public WheelParticles wheelParticles;
    public GameObject smokePrefab;
    public WheelsColliders colliders;

    private void Start() {
        InstantiateSmoke();
    }

    private void Update()
    {
        ManagerDrift();
        CheckParticles();
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
            }
        }
        else 
        {
            if(isDrifting && stopDriftCoroutine == null)
            {
                StopDrift();
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

    void InstantiateSmoke()
    {
        wheelParticles.FRWheel = Instantiate(smokePrefab, colliders.FR.transform.position-Vector3.up*colliders.FR.radius, Quaternion.identity, colliders.FR.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.FLWheel = Instantiate(smokePrefab, colliders.FL.transform.position- Vector3.up * colliders.FR.radius, Quaternion.identity, colliders.FL.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.RRWheel = Instantiate(smokePrefab, colliders.RR.transform.position- Vector3.up * colliders.FR.radius, Quaternion.identity, colliders.RR.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.RLWheel = Instantiate(smokePrefab, colliders.RL.transform.position- Vector3.up * colliders.FR.radius, Quaternion.identity, colliders.RL.transform)
            .GetComponent<ParticleSystem>();
    }
    void CheckParticles() 
    {
        WheelHit[] wheelHits = new WheelHit[4];
        colliders.FR.GetGroundHit(out wheelHits[0]);
        colliders.FL.GetGroundHit(out wheelHits[1]);

        colliders.RR.GetGroundHit(out wheelHits[2]);
        colliders.RL.GetGroundHit(out wheelHits[3]);

        float slipAllowance = 0.5f;
        if ((Mathf.Abs(wheelHits[0].sidewaysSlip) + Mathf.Abs(wheelHits[0].forwardSlip) > slipAllowance)){
            wheelParticles.FRWheel.Play();
        }
        else
        {
            wheelParticles.FRWheel.Stop();
        }
        if ((Mathf.Abs(wheelHits[1].sidewaysSlip) + Mathf.Abs(wheelHits[1].forwardSlip) > slipAllowance)){
            wheelParticles.FLWheel.Play();
        }
        else
        {
            wheelParticles.FLWheel.Stop();
        }
        if ((Mathf.Abs(wheelHits[2].sidewaysSlip) + Mathf.Abs(wheelHits[2].forwardSlip) > slipAllowance)){
            wheelParticles.RRWheel.Play();
        }
        else
        {
            wheelParticles.RRWheel.Stop();
        }
        if ((Mathf.Abs(wheelHits[3].sidewaysSlip) + Mathf.Abs(wheelHits[3].forwardSlip) > slipAllowance)){
            wheelParticles.RLWheel.Play();
        }
        else
        {
            wheelParticles.RLWheel.Stop();
        }


    }
}

[System.Serializable]
public class WheelParticles{
    public ParticleSystem FRWheel;
    public ParticleSystem FLWheel;
    public ParticleSystem RRWheel;
    public ParticleSystem RLWheel;

}
[System.Serializable]
public class WheelsColliders
    {
        public WheelCollider FR;
        public WheelCollider FL;
        public WheelCollider RR;
        public WheelCollider RL;
    }