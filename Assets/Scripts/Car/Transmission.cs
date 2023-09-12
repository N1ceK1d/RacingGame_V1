using TMPro;
using UnityEngine;

public class Transmission : MonoBehaviour
{

    public TransmissionType type;
    public float[] gearRatios;
    public float currentRatio;
    public int currentGear = 0;
    public float mainGearRatio;
    public TMP_Text currentGearText;

    public float minSpeedGear;
    public float maxSpeedGear;

    public void AutomatTransmission(float speed)
    {
        if(speed >= maxSpeedGear)
        {
            BoostGear();
        }
        if(speed <= minSpeedGear)
        {
            LowerGear();
        }
    }

    public void ManualTransmission()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            BoostGear();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            LowerGear();
        }
    }

    public float BoostGear()
    {
        if(currentGear < gearRatios.Length - 1)
        {
            currentGear += 1;
            currentRatio = gearRatios[currentGear];
        }
        currentGearText.text = currentGear.ToString();
        return gearRatios[currentGear];
        
    }

    public float LowerGear()
    {
        if(currentGear >= 0)
        {
            currentGear -= 1;
            currentRatio = gearRatios[currentGear];
        }
        currentGearText.text = currentGear.ToString();
        return gearRatios[currentGear];
    }
}

public enum TransmissionType
{
    manual,
    automat
}