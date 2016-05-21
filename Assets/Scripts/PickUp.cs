using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    private Animator anim;
    GameObject car;
    Transform target;
    float stopRange;

    public float moneyToGive = 0.0f;

    public static float lastButtonPress;

    public string pickUpText = "PRESS <key>ENTER</key> TO PICK UP PASSENGER";

    public static bool displayingNotification = false;

    // Use this for initialization
    void Start()
    {
        car = GameObject.Find("Auto_RikshawRusted");
        target = GameObject.Find("Auto_RikshawRusted").transform;
        anim = GetComponent<Animator>();
        stopRange = 0.1f;
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.Equals(PassengerSystem.NextPassengerPosition))
        {
            anim.SetBool("inRange", Vector3.Distance(transform.position, target.position) < stopRange*4);

            if(DropOff.hasPassanger == false && Vector3.Distance(transform.position, target.position) < stopRange)
            {
                GameNotification.Reset();
                GameNotification.Message = pickUpText;
                displayingNotification = true;
                GameNotification.ShowNotification();
            } else if(DropOff.displayingNotification)
            {
                displayingNotification = false;
                GameNotification.HideNotification(pickUpText);
            } else if(DropOff.hasPassanger == false && Vector3.Distance(transform.position, target.position) > stopRange)
            {
                displayingNotification = false;
                GameNotification.HideNotification(pickUpText);
            }

            if (DropOff.hasPassanger == false && (Time.time - lastButtonPress) > InputConstants.MENU_ACTION_DELAY && (Input.GetKeyDown(KeyCode.E) || theController.isButtonPressed(theController.STATE_OPTION1)) && (Vector3.Distance(transform.position, target.position) < stopRange))
            {
                GameNotification.HideNotification(pickUpText);

                DropOff.hasPassanger = true;
                DropOff.moneyToGive = moneyToGive;
                print("passanger boarded");

                Vector3 destination = PassengerSystem.NextPassengerDestination;

                NavigationHandler.targetPosition = new Vector3(destination.x, destination.y, destination.z);
                lastButtonPress = Time.time;
                Destroy(gameObject);
            }
        }

    }
}