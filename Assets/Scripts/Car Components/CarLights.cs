using UnityEngine;
using System.Collections;

[System.Serializable]
public class CarLights : MonoBehaviour
{
    public Light[] parkingLights; // Габаритные огни
    public Light[] leftTurn; // Левые поворотники
    public Light[] rightTurn; // Правые поворотники
    public Light[] frontLights; // Передние фары
    public Light[] stopLights; // Остановочные огни
    public IEnumerator coroutine;

    public void TurnSignals(ref bool leftLight, ref bool rightLight, Light[] turnsLights)
    {
        
        leftLight = false;
        rightLight = !rightLight;
        coroutine = FlashingLight(turnsLights, rightLight);
        if(rightLight == true)
        {
            StartCoroutine(coroutine);
            Debug.Log("Turn start");
        }
    }

    private IEnumerator FlashingLight(Light[] light, bool lighting)
    {
        while(lighting)
        {
            float intensiteNum = Mathf.Lerp(0, 50, Mathf.PingPong(Time.time, 0.3f));
            foreach(Light turnLight in light)
            {
                turnLight.intensity = intensiteNum;
            }
            yield return intensiteNum;
        }

        foreach(Light turnLight in light)
        {
            turnLight.intensity = 0;
        }
        yield return 0;
    }

    public void OffTurnSignals(ref bool leftLight, ref bool rightLight)
    {
        leftLight = false;
        rightLight = false;
        foreach(Light turnLight in leftTurn)
        {
            turnLight.intensity = 0;
        }
        foreach(Light turnLight in rightTurn)
        {
            turnLight.intensity = 0;
        }
    }

    public void Headlights(ref bool frontLight)
    {
        if(Input.GetKey(KeyCode.L))
        {
            frontLight = !frontLight;
            if(frontLight)
            {
                foreach(Light light in frontLights)
                {
                    light.intensity = 3;
                }
            } 
            else
            {
                foreach(Light light in frontLights)
                {
                    light.intensity = 0;
                }
            }
        }
    }

    public void ParkingLights(ref bool parkingLight)
    {
        if(Input.GetKey(KeyCode.P))
        {
            parkingLight = !parkingLight;
            if(parkingLight)
            {
                foreach(Light light in parkingLights)
                {
                    light.intensity = 3;
                }
            } 
            else
            {
                foreach(Light light in parkingLights)
                {
                    light.intensity = 0;
                }
            }
        }
    }

    public void StopSignals(bool isBraking, Light[] stopLights)
    {
        if(isBraking)
        {
            foreach(Light light in stopLights)
            {
                light.intensity = 3;
            }
        }
        else
        {
            foreach(Light light in stopLights)
            {
                light.intensity = 0;
            }
        }
    }
}