using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarSuspension : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float dumperStiffness;

    private float maxLength;
    private float minLength;
    private float lastLength;
    private float springLength;
    private float springVelocity;
    private float springForce;
    private float dumperForce;

    [Header("Wheel")]
    public float wheelRadius;
    public float wheelBase;
    public float turnRadius;
    public float rearTrack;
    public bool isDrive;

    private Vector3 suspensionForce;
    private Vector3 wheelVelocityLS;
    private float Fx;
    private float Fy;

    public float steerAngle;


    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y + steerAngle, transform.localRotation.z);
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            springForce = springStiffness * (restLength - springLength);
            dumperForce = dumperStiffness * springVelocity;
            suspensionForce = (springForce + dumperForce) * transform.up;

            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            Fx = Input.GetAxis("Vertical") * springForce;
            Fy = wheelVelocityLS.x * springForce;
            if(isDrive)
            {
                rb.AddForceAtPosition(suspensionForce + (Fx * transform.forward) + (Fy * -transform.right), hit.point);
            }   
            else 
            {
                rb.AddForceAtPosition(suspensionForce, hit.point);
            }

            

            Debug.DrawRay(transform.position, -transform.up * springLength, Color.red);
        }
    }
}
