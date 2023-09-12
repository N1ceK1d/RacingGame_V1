using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tachometer : MonoBehaviour
{
    public GameObject target;
    public TMP_Text speedLabel;
    public Image tachometer;

   
    private float speed;
    private Rigidbody targetRb;
    private Car_Controller controller;
    
    private void Start() {
        targetRb = target.GetComponent<Rigidbody>();
        controller = target.GetComponent<Car_Controller>();
    }

    void FixedUpdate()
    {

        speed = Mathf.RoundToInt(targetRb.velocity.magnitude * 3.6f);
        speedLabel.text = speed + " km/h";
        tachometer.fillAmount = Mathf.Lerp(0, 1, speed / controller.maxSpeed);
    }
}
