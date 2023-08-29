using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    public Wheel FR;
    public Wheel FL;
    public Wheel RR;
    public Wheel RL;

    public float wheelCircle;
    
    private void Update() {
            UpdateWheelMeshes(FR.wheelCollider, FR.wheelMesh);
            UpdateWheelMeshes(FL.wheelCollider, FL.wheelMesh);
            UpdateWheelMeshes(RR.wheelCollider, RR.wheelMesh);
            UpdateWheelMeshes(RL.wheelCollider, RL.wheelMesh);
    }

    public void WheelEffectsStart()
    {
        FR.wheelMark.emitting = true;
        FL.wheelMark.emitting = true;
        RR.wheelMark.emitting = true;
        RL.wheelMark.emitting = true;

        RR.wheelSmoke.Play();
        RL.wheelSmoke.Play();
    }

    public void WheelEffectsStop()
    {
        FR.wheelMark.emitting = false;
        FL.wheelMark.emitting = false;
        RR.wheelMark.emitting = false;
        RL.wheelMark.emitting = false;

        RR.wheelSmoke.Stop();
        RL.wheelSmoke.Stop();
    }

    void UpdateWheelMeshes(WheelCollider col, MeshRenderer mesh)
    {
        Quaternion quat;
        Vector3 position;
        col.GetWorldPose(out position, out quat);
        mesh.transform.position = position;
        mesh.transform.rotation = quat;
    }
}

[System.Serializable]
public class Wheel{
    public MeshRenderer wheelMesh;
    public ParticleSystem wheelSmoke;
    public WheelCollider wheelCollider;
    public TrailRenderer wheelMark;
}
