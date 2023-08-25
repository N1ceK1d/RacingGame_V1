using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] [Range(0, 99999999)] private int money;
    [SerializeField] private Text playerName;
    [SerializeField] private Image animal;
    [SerializeField] private GameObject[] cars;
    public Text playerMoney;

    public CarTachometer carTachometer;

    private bool isCarUsed = true;
    private bool canChange = false;

    public CameraController carCamera;
    private void Start() {
        cars[0].GetComponent<CarController>().enabled = true;
        cars[1].GetComponent<CarController>().enabled = false;
        ChangeCarHandler(cars[0], cars[1]);
    }
    void Update()
    {
        SetPlayerData();
        if(cars.Length > 1)
        {
            ChangeCar();
        }
    }

    public void SetPlayerData()
    {
        playerName.text = PlayerData.GetName();
        animal.sprite = PlayerData.playerAnimal;
        playerMoney.text = money + "¥";
    }

    public void ChangeCar()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isCarUsed = !isCarUsed;
            if(isCarUsed)
            {
                cars[0].GetComponent<CarController>().enabled = true;
                cars[1].GetComponent<CarController>().enabled = false;
                ChangeCarHandler(cars[0], cars[1]);
            }
            else
            {
                cars[0].GetComponent<CarController>().enabled = false;
                cars[1].GetComponent<CarController>().enabled = true;
                ChangeCarHandler(cars[1], cars[0]);
            }
        }
    }

    public void ChangeCarHandler(GameObject currentCar, GameObject unusedCar)
    {
        carCamera.player = currentCar.GetComponent<Transform>();
        carTachometer.target = currentCar.GetComponent<Rigidbody>();
        carTachometer.maxSpeed = currentCar.GetComponent<CarEngine>();
        unusedCar.GetComponent<CarController>().engine.isStarted = false;
        unusedCar.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("CanChange" + canChange);
    }
}
