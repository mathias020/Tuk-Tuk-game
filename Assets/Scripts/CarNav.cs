using UnityEngine;
using System.Collections;

public class CarNav : MonoBehaviour {

	public static float currentSpeed = 0; //Current_speed
    public float power;                    //current power (acceleration)
	public float friction = 1.50f;         //friction
	
	public float turnSpeed;                //turn speed = 10
    public float breakPower = 0.01f;       //break 

    public static float maxSpeed = 0.005f;         //maxSpeed

	public float fuel = 5000;
    private float lastStearing = 0.0f;     //time

	
	// Update is called once per frame
	void Update () {
        if (!GameOverOverlay.GameIsOver && fuel > 0 && theController.getState(theController.STATE_START))
        {
            checkPassangerPickUp();
            checkHorn();
            checkPause();

            //Get acceleration
            if ( (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || theController.getAcceleration() <= InputConstants.CARNAV_ACCELERATION_THRESHOLD)) //  -1.0f to 1.0f, bad reading on the board => >-0.98f
            {
                power = 0.0005f;
            }
            else
                power = 0;

            //Tuk Tuk state and break
            if ( Input.GetKey(KeyCode.Space) || theController.getState(theController.STATE_BREAK))
            {
                Debug.Log("Breaking");
                if (currentSpeed > 0)
                {
                    if (currentSpeed - breakPower >= 0)
                    {
                        currentSpeed -= breakPower;
                    }
                    else
                        currentSpeed = 0;

                }
                if (currentSpeed < 0)
                {
                    if (currentSpeed + breakPower <= 0)
                    {
                        currentSpeed += breakPower;
                    }
                    else
                        currentSpeed = 0;
                }
            }
            else {
                if ( (Input.GetKey(KeyCode.W) || theController.getState(theController.STATE_FORWARD)) && currentSpeed >= 0)
                {
                    if (currentSpeed < maxSpeed)
                    {
                        currentSpeed += power;
                        SpeedDialHandler.UpdateSpeed();
                        //fuel -= (power * 10);
                    }
                }

                if ( (Input.GetKey(KeyCode.S) || theController.getState(theController.STATE_REVERSE)) && currentSpeed <= 0)
                { 
                    if (currentSpeed > -(maxSpeed / 2))
                    {
                        currentSpeed -= power;
                        SpeedDialHandler.UpdateSpeed();
                        //fuel -= (power * 10);
                    }
                }
            }

            int invert = theController.getState(theController.STATE_FORWARD) ? 1 : -1;

            //Turning
            if ( (Input.GetKey(KeyCode.A) || (theController.getStearing() < InputConstants.CARNAV_STEARING_THRESHOLD_LEFT)) && Mathf.Abs(currentSpeed) > 0 && (Time.time - lastStearing) > InputConstants.CARNAV_STEARING_DELAY)
            {
                transform.Rotate(Vector3.up, (-turnSpeed * invert * Mathf.Pow((Mathf.Abs(theController.getStearing())), InputConstants.CARNAV_EXPONENTIAL_INCLINE) ) * Time.deltaTime);
                lastStearing = Time.time;
            }


            else if ((Input.GetKey(KeyCode.D) || (theController.getStearing() > InputConstants.CARNAV_STEARING_THRESHOLD_RIGHT)) && Mathf.Abs(currentSpeed) > 0 && (Time.time - lastStearing) > InputConstants.CARNAV_STEARING_DELAY)
            {
                transform.Rotate(Vector3.up, (turnSpeed * invert * Mathf.Pow((Mathf.Abs(theController.getStearing())), InputConstants.CARNAV_EXPONENTIAL_INCLINE)) * Time.deltaTime);
                lastStearing = Time.time;
            } else
            {
                lastStearing = Time.time;
            }
            //fuel;
            if (fuel < 0)
            {
                currentSpeed = 0;
                Debug.Log("Out of fuel: " + fuel);
            }

            SpeedDialHandler.UpdateSpeed();

            //friction
            if (currentSpeed * 100 > InputConstants.CARNAV_FRICTION_THRESHOLD || currentSpeed * 100 < InputConstants.CARNAV_FRICTION_THRESHOLD_NEGATIVE)
                currentSpeed *= friction;
            else if ((currentSpeed * 100 >= InputConstants.CARNAV_FRICTION_THRESHOLD_ZERO && currentSpeed * 100 <= InputConstants.CARNAV_FRICTION_THRESHOLD) || (currentSpeed * 100 <= InputConstants.CARNAV_FRICTION_THRESHOLD_ZERO && currentSpeed * 100 >= InputConstants.CARNAV_FRICTION_THRESHOLD_NEGATIVE))
            {
                currentSpeed = 0;
            }

            //moving

            transform.Translate(Vector3.right * currentSpeed);
        }
        
    }

    private void checkPassangerPickUp()
    {
        if (theController.getState(theController.STATE_OPTION1))
        {
            //pick up Passanger;            
        }
    }
    private void checkPause()
    {
        if (theController.getState(theController.STATE_OPTION2))
        {
            //Pause game, main menu
        }
    }
    private void checkHorn()
    {
        if (theController.getState(theController.STATE_OPTION3))
        {
            //horn;
        }
    }
}
