[System.Serializable]
public class CarTransmission 
{
    public Gear[] gears;
    public Gear currentGear;
    public float mainGearRatio;
    public int gearNumber = 0;
    public float BoostGear()
    {
        if(gearNumber < gears.Length - 1)
        {
            gearNumber += 1;
            currentGear = gears[gearNumber];
        }
        return currentGear.gearRatio;
    }

    public float LowerGear()
    {
        if(gearNumber >= 0)
        {
            gearNumber -= 1;
            currentGear = gears[gearNumber];
        }
        return currentGear.gearRatio;
    }

}

[System.Serializable]
public class Gear
{
    public float gearRatio;
    /*
        ---Toyota AE86-----
        1 передача 3.545
        2 передача 1.904
        3 передача 1.310
        4 передача 0.969
        5 передача 0.815
        Задняя передача 3.250
        Передаточное число дифференциала 4.058
    
        ---Toyota Supra------
        1 передача	5.25
        2 передача	3.36
        3 передача	2.172
        4 передача	1.72
        5 передача	1.316
        6 передача	1
        7 передача	0.822
        8 передача	0.64
        Задняя передача	3.712
        Передаточное число дифференциала 3.154
    */
}