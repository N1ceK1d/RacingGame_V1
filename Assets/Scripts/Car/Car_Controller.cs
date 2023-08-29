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
    void Start()
    {
        car = GetComponent<Rigidbody>();
        car.centerOfMass = centerOfMass.transform.localPosition;
    }

    void Update()
    {
        carSpeed = car.velocity.magnitude * 3.6f;
        speedClamped = Mathf.Lerp(speedClamped, carSpeed, Time.deltaTime);

        steeringSystem.SetSteering(Input.GetAxis("Horizontal"));
        //engine.MaxSpeed(wheel.wheelCircle, transmission.mainGearRatio, transmission.currentGearRation);
        //engine.LimitSpeed(car);

        //if(transmission.transmissionType == Transmission.TransmissionType.AKPP)
        //{
        //    transmission.AutomaticalChangeGear();
        //}
        //else 
        //{
        //    if(Input.GetKeyDown(KeyCode.UpArrow))
        //    {
        //        transmission.BoostGear();
        //    }
        //    if(Input.GetKeyDown(KeyCode.DownArrow))
        //    {
        //        transmission.ReducedGear();
        //    }
        //}
    }

    public float GetSpeedRatio()
    {
        float gasInput = Input.GetAxis("Vertical");
        var gas = Mathf.Clamp(Mathf.Abs(gasInput), 0.5f, 1f);
        return speedClamped * gas / 350;
    }

}
