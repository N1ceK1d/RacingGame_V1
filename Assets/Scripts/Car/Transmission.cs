using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transmission : MonoBehaviour
{
    public enum TransmissionType
    {
        AKPP, MKPP
    }
    public Engine engine;
    public TransmissionType transmissionType;
    public List<Gear> gears;
    public float mainGearRatio;
    public float currentGearRation;
    public Text currentGearText;
    public AudioSource changeGearSound;

    private Gear currentGear;
    private int gearIndex;
    

    public void BoostGear()
    {
        gearIndex++;
        currentGear = gears[gearIndex];
        changeGearSound.Play();
    }

    public void ReducedGear()
    {
        changeGearSound.Play();
    }

    public void AutomaticalChangeGear()
    {
        if(engine.currentSpeed >= engine.maxSpeed)
        {
            BoostGear();
        }
    }


}

[System.Serializable]
public class GetSpeedRatio{
    public string gearValue;
    public float gearRatio;
}
