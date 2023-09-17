using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    public float wheelCircle;
    public AudioSource driftSound;
    public Material skidMarkMaterial;
    public ParticleSystem tireSmoke;
    public Wheel FR;
    public Wheel FL;
    public Wheel RR;
    public Wheel RL;

    public float alpha;
    public float duration;
    
    private void Update() {
        UpdateWheelMeshes(FR.wheelCollider, FR.wheelMesh);
        UpdateWheelMeshes(FL.wheelCollider, FL.wheelMesh);
        UpdateWheelMeshes(RR.wheelCollider, RR.wheelMesh);
        UpdateWheelMeshes(RL.wheelCollider, RL.wheelMesh);
        skidMarkMaterial.color = new Color(0f, 0f, 0f, alpha);
    }

    public void WheelEffectsStart()
    {
        StartCoroutine(smoothVal(0.0f, 0.3f, duration));
        if(FR.wheelCollider.isGrounded)
        {
            FR.wheelMark.emitting = true;
        }
        if(FL.wheelCollider.isGrounded)
        {
            FL.wheelMark.emitting = true;
        }
        if(RR.wheelCollider.isGrounded)
        {
            RR.wheelMark.emitting = true;
            if(alpha > .2f)
            {
                RR.wheelSmoke.Play();
            }
        }
        if(RL.wheelCollider.isGrounded)
        {
            if(alpha > .2f)
            {
                RL.wheelSmoke.Play();
            }
            RL.wheelMark.emitting = true;
        }
        if(RR.wheelCollider.isGrounded || RL.wheelCollider.isGrounded)
        {
            driftSound.Play();
        }
    }

    IEnumerator smoothVal (float from, float to, float timer)
    { 
        float t = 0.0f;
        float val = from;
        while (t < 1.0f) {
            t += Time.deltaTime * (1.0f / timer);
            val = Mathf.Lerp (from, to, t); 
            alpha = val;
            yield return 0;
        }
    }

    public void WheelEffectsStop()
    {
        FR.wheelMark.emitting = false;
        FL.wheelMark.emitting = false;
        RR.wheelMark.emitting = false;
        RL.wheelMark.emitting = false;

        RR.wheelSmoke.Stop();
        RL.wheelSmoke.Stop();
        driftSound.Stop();
    }

    void UpdateWheelMeshes(WheelCollider col, MeshRenderer mesh)
    {
        Quaternion quat;
        Vector3 position;
        col.GetWorldPose(out position, out quat);
        mesh.transform.position = position;
        mesh.transform.rotation = quat;
        
        //mesh.transform.localEulerAngles = new Vector3 (quat.x, quat.y + -90, quat.z + 90);
    }
}

[System.Serializable]
public class Wheel{
    public MeshRenderer wheelMesh;
    public ParticleSystem wheelSmoke;
    public WheelCollider wheelCollider;
    public TrailRenderer wheelMark;
}
