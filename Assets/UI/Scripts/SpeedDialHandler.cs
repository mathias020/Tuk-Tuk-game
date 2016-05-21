using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedDialHandler : MonoBehaviour {

    private static Text speed_value;

    public static int kmhMaxSpeed = 50;

    private static int speed;

    public static int Speed
    {
        get { return speed; }
    }



    public static void UpdateSpeed()
    {
        /*
        speed = (int)( ( (CarNav.maxSpeed) / (CarNav.currentSpeed) ) * kmhMaxSpeed );
        if (speed > 50)
            speed = 50;
        if (speed < 0)
            speed = 0;
        if (speed_value != null)
            speed_value.text = "" + speed;
        else
            Debug.Log("speed_value is null");
            */
            
    }

	// Use this for initialization
	void Start () {
        speed_value = GameObject.Find("speed_value").GetComponent<Text>();
	}

    void Update()
    {
        speed = (int)(((CarNav.maxSpeed) / (CarNav.currentSpeed)) * kmhMaxSpeed);
        if (speed > 50)
            speed = 50;
        if (speed < 0)
            speed = 0;
        if (speed_value != null)
            speed_value.text = "" + speed;
        else
            Debug.Log("speed_value is null");
    }
}
