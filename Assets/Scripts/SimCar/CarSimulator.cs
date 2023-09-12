using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSimulator : MonoBehaviour
{
    struct Engine
{
    // базовые величины
    float[] Torque;
    float Power;
    float Inertia, Mass, BackTorque;
    char Direction;

    float IdleRPM; // обороты холостого хода

    // динамические величины
    float RPM;

    // функция обновления
    void Progress(float Throttle, float Clutch)
    {
        if (RPM < IdleRPM && Throttle < 0.3f)
            Throttle += 0.1f;

        float torq = Torque[ (int)RPM ] * Throttle;

        float additionRPM = torq / Inertia - Mathf.Pow(1.0f - Throttle, 2) * BackTorque;
        RPM += additionRPM * (1.0f - Clutch);
    }
};
}
