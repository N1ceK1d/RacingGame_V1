using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tachometer : MonoBehaviour
{
    
    public Rigidbody target;
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public Text speedLabel;
    public RectTransform arrow;
   
    private float speed;

    void FixedUpdate()
    {
        speed = Mathf.RoundToInt(target.velocity.magnitude * 3.6f);

        if(speedLabel != null)
        {
            speedLabel.text = ((int)speed) + " km/h";
            //speedLabel.text = (int)speed + "";
        } 
        if(arrow != null)
        {
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / 350));
        }
    }
}
