using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CarController : MonoBehaviour
{

    [Header("Car Specs")]
    public float wheelBase;
    public float turnRadius;
    public float rearTrack;
    public float wheelCircle;
    public GameObject centerOfMass;

    [Header("Engine")]
    public CarEngine engine; 

    [Header("Transmission")]
    public CarTransmission transmission;
    public Text gearNum;

    [Header("Wheels")]
    public WheelColliders colliders;
    public WheelMeshes meshes;
    public WheelCollider[] driveWheels;

    [Header("Car lights")]
    public CarLights carLights;
    private bool leftLight = false;
    private bool rightLight = false;
    private bool frontLights = false;
    private bool parkingLights = false;
    private Rigidbody rb;

    private float speedClamped;
    private float carSpeed;

    private float gasInput;

    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }
    void FixedUpdate()
    {
        carSpeed = rb.velocity.magnitude * 3.6f;
        SteerWheels();
        engine.EngineOnOff();
        if(engine.isStarted)
        {
            Gas();
        }
        else 
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 0);
        }
        engine.MaxSpeed(wheelCircle, transmission.mainGearRatio, transmission.currentGear.gearRatio);
        engine.LimitSpeed(rb);
        BrakingSystem();
    }
    void Update()
    {
        DoLight();
        speedClamped = Mathf.Lerp(speedClamped, carSpeed, Time.deltaTime);
        ChangeGear();
    }

    private void DoLight()
    {
        if(Input.GetKey(KeyCode.E))
        {
            carLights.TurnSignals(ref leftLight, ref rightLight, carLights.rightTurn);
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            carLights.TurnSignals(ref rightLight, ref leftLight, carLights.leftTurn);
        }
        else
        {
            carLights.StopAllCoroutines();
            carLights.OffTurnSignals(ref leftLight, ref rightLight);
        }
        carLights.Headlights(ref frontLights);
        carLights.ParkingLights(ref parkingLights);
    }
    private void ChangeGear()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transmission.BoostGear();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transmission.LowerGear();
        }
        if(transmission.gearNumber == 0) 
        {
            gearNum.text = "R";
        } 
        else 
        { gearNum.text = transmission.gearNumber.ToString();}
    }
    private void BrakingSystem()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Handbrake();
        }
        else if(Input.GetKey(KeyCode.S))
        {
            PedalBraking();
        }
        else
        {
            carLights.StopSignals(false, carLights.stopLights);
            colliders.RRWheel.brakeTorque = 0;
            colliders.RLWheel.brakeTorque = 0;
            colliders.FRWheel.brakeTorque = 0;
            colliders.FLWheel.brakeTorque = 0;
        }
    }
    void SteerWheels()
    {
        // формула угла Аккермана -
        // ∠α = arctg (0.5 ⋅ X ÷ Y)
        // ∠β = arctg(Y ÷ (0.5 ⋅ X))
        float steerInput = Input.GetAxis("Horizontal");

        float ackermanAngleLeft;
        float ackermanAngleRight;
        if(steerInput > 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if(steerInput < 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
        }
        else
        {
            ackermanAngleLeft = 0;
            ackermanAngleRight = 0;
        }
        colliders.FRWheel.steerAngle = ackermanAngleRight;
        colliders.FLWheel.steerAngle = ackermanAngleLeft;
        UpdateWheelMeshes(colliders.FRWheel, meshes.FRWheel);
        UpdateWheelMeshes(colliders.FLWheel, meshes.FLWheel);
        UpdateWheelMeshes(colliders.RRWheel, meshes.RRWheel);
        UpdateWheelMeshes(colliders.RLWheel, meshes.RLWheel);
    }
    void UpdateWheelMeshes(WheelCollider col, MeshRenderer mesh)
    {
        Quaternion quat;
        Vector3 position;
        col.GetWorldPose(out position, out quat);
        mesh.transform.position = position;
        mesh.transform.rotation = quat;
    }
    public void Gas()
    {
        gasInput = Mathf.Abs(Input.GetAxis("Vertical"));
        float torque;

        torque = (engine.enginePower * 9550 / (engine.GetRPM(gasInput)));
        if(transmission.gearNumber > 0)
        {
            foreach (WheelCollider wheel in driveWheels)
            {
                wheel.motorTorque = torque * gasInput;
            }
        }
        else 
        {
            foreach (WheelCollider wheel in driveWheels)
            {
                wheel.motorTorque = torque * (gasInput * -1);
            }
        }
    }
    public float GetSpeedRatio()
    {
        var gas = Mathf.Clamp(Mathf.Abs(gasInput), 0.5f, 1f);
        return speedClamped * gas / engine.maxSpeed;
    }

    void Handbrake()
    {
        colliders.RRWheel.brakeTorque = 500000;
        colliders.RLWheel.brakeTorque = 500000;
    }

    void PedalBraking()
    {
        carLights.StopSignals(true, carLights.stopLights);
        colliders.RRWheel.brakeTorque = 2500;
        colliders.RLWheel.brakeTorque = 2500;
        colliders.FRWheel.brakeTorque = 2500;
        colliders.FLWheel.brakeTorque = 2500;
    }
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}

[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}
