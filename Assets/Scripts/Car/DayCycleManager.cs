using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DayCycleManager : MonoBehaviour
{
    [SerializeField] Gradient directionLightGradient;
    [SerializeField] Gradient ambientLightGradient;

    [SerializeField, Range(1, 3600)] float timeDayInSecond = 60f;
    [SerializeField, Range(0f, 1f)] float timeProgress;

    Vector3 defaultAngles;

    [SerializeField] Light directionLight;
    private void Start() {
        defaultAngles = directionLight.transform.localEulerAngles;
    }

    private void Update() {
        if(Application.isPlaying)
            timeProgress += Time.deltaTime / timeDayInSecond;

        if(timeProgress > 1)
            timeProgress = 0;

        directionLight.color = directionLightGradient.Evaluate(timeProgress);
        RenderSettings.ambientLight = ambientLightGradient.Evaluate(timeProgress);

        directionLight.transform.localEulerAngles = new Vector3(360f * timeProgress - 90, defaultAngles.x, defaultAngles.z);
    }
}