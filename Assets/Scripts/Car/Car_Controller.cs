using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    private Rigidbody car;
    public GameObject centerOfMass;

    [Header("Car components")]
    public Transmission transmission;
    public SteeringSystem steeringSystem;
    public AccelerationSystem accelerationSystem;
    public Engine engine;
    public Wheels wheel;

    private float speedClamped;
    private float carSpeed;
    public float maxSpeed;

    private RaycastHit hit;

    private Vector3 wheelVelocityLS; //LocalSpace
    private float Fx;
    private float Fy;
    public float springForce;
    public Vector3 suspensionForce;
    void Start()
    {
        car = GetComponent<Rigidbody>();
        car.centerOfMass = centerOfMass.transform.localPosition;
    }

    void Update()
    {
        carSpeed = car.velocity.magnitude * 3.6f;
        speedClamped = Mathf.Lerp(speedClamped, carSpeed, Time.deltaTime);

        engine.MaxSpeed(wheel.wheelCircle, transmission.mainGearRatio, transmission.currentRatio);
        maxSpeed = engine.maxSpeed;
        engine.LimitSpeed(car);
        engine.CalculateEngineTorque();
        accelerationSystem.torque = engine.CalculateWheelTorque(engine.engineTorque, transmission.currentRatio, transmission.mainGearRatio);
        accelerationSystem.Handbrake(carSpeed);

        if(transmission.type == TransmissionType.manual)
        {
            transmission.ManualTransmission();
        }
        else 
        {
            transmission.maxSpeedGear = engine.maxSpeed;
            transmission.AutomatTransmission(carSpeed);
        }

        steeringSystem.SetSteering(Input.GetAxis("Horizontal"));
        
        Debug.DrawRay(transform.position, -transform.up * 100, Color.red);
        wheelVelocityLS = transform.InverseTransformDirection(car.GetPointVelocity(hit.point));

        Fx = Input.GetAxis("Vertical") * springForce;
        Fy = wheelVelocityLS.x * springForce;

        car.AddForceAtPosition(suspensionForce + (Fx * transform.forward) + (Fy * -transform.right), hit.point);
    }

    public float currentSpeed ()
    {
        float speed = car.velocity.magnitude;
        speed = car.velocity.magnitude * 3.6f;
        return speed;
    }

    public float GetSpeedRatio()
    {
        float gasInput = Input.GetAxis("Vertical");
        var gas = Mathf.Clamp(Mathf.Abs(gasInput), 0.5f, 1f);
        return speedClamped * gas / 350;
    }

}
