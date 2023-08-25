using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public enum CameraFollowType
    {
        car,
        world
    }
    public CameraFollowType cameraFollow;

    public GameObject atachedVehicle;

    public  GameObject cameraFolder;
    public  Transform[] camLocations;
    public  int locationIndicator = 2;

    [Range(0, 1)] public float smoothTime = .5f;

    private float X, Y, Z;
    public int speeds;
    private float eulerX=0, eulerY=0;

    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        atachedVehicle = GameObject.FindGameObjectWithTag("Player");
        cameraFolder = atachedVehicle.transform.Find("Camera").gameObject;
        camLocations = cameraFolder.GetComponentsInChildren<Transform>();
    }

    void Update() {
        if(cameraFollow == CameraFollowType.car)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                if(locationIndicator > 4 || locationIndicator < 2) locationIndicator = 2;
                else locationIndicator+=1;
            }
        }
        
    }

    void FixedUpdate() {
        if(cameraFollow == CameraFollowType.car)
        {
            transform.position = camLocations[locationIndicator].position * (1 - smoothTime) + transform.position * smoothTime;
            transform.LookAt(camLocations[1].transform);
        } 
        else 
        {
            X = Input.GetAxis("Mouse X") * speeds * Time.deltaTime;
            Y = -Input.GetAxis("Mouse Y") * speeds * Time.deltaTime;
            eulerX = (transform.rotation.eulerAngles.x + Y) % 360;
            eulerY = (transform.rotation.eulerAngles.y + X) % 360;
                transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
            if (Input.GetKeyUp (KeyCode.Escape)) {
              Cursor.lockState = CursorLockMode.None;
            }
        }

    }
}
