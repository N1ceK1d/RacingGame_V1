using UnityEngine;
using UnityEngine.UI;

public class CarTachometer : MonoBehaviour
{
    public Rigidbody target;
    public CarEngine maxSpeed;
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public Text speedLabel;
    public RectTransform arrow;
   
    private float speed;

    void FixedUpdate()
    {
        if(maxSpeed.isStarted)
        {
            minSpeedArrowAngle = 0;
        }
        else
        {
            minSpeedArrowAngle = 10;
        }
        speed = Mathf.RoundToInt(target.velocity.magnitude * 3.6f);

        if(speedLabel != null)
        {
            //speedLabel.text = ((int)speed) + " km/h";
            speedLabel.text = (int)speed + "";
        } 
        if(arrow != null)
        {
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed.maxSpeed));
        }
    }
}
