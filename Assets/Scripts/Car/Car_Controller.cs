using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    private Rigidbody car;
    public GameObject centerOfMass;
    void Start()
    {
        car = GetComponent<Rigidbody>();
        car.centerOfMass = centerOfMass.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
