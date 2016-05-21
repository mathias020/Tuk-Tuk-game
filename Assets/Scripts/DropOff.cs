using UnityEngine;
using System.Collections;

public class DropOff : MonoBehaviour {

	public GameObject human;
	public static bool hasPassanger = false;
    public static float moneyToGive;

    public string dropOffText = "PRESS <key>ENTER</key> TO DROP OFF PASSENGER";

    public static float lastMoneyRemoval = 0;

    public static bool displayingNotification = false;
	
	// Update is called once per frame
	void Update () {
        if(hasPassanger == true && Vector3.Distance(transform.position, NavigationHandler.targetPosition) < 0.1f)
        {
            GameNotification.Reset();
            GameNotification.Message = dropOffText;
            displayingNotification = true;
            GameNotification.ShowNotification();
            
        } else if(!PickUp.displayingNotification)
        {
            displayingNotification = false;
            GameNotification.HideNotification(dropOffText);
        } else if(hasPassanger == true && Vector3.Distance(transform.position, NavigationHandler.targetPosition) > 0.1f)
        {
            displayingNotification = false;
            GameNotification.HideNotification(dropOffText);
        }

        if(DropOff.hasPassanger && Time.time - lastMoneyRemoval > 20.0f)
        {
            moneyToGive -= 1.0f;
            lastMoneyRemoval = Time.time;
        }

		if (Vector3.Distance(transform.position, NavigationHandler.targetPosition) < 0.1f && hasPassanger == true && (Time.time - PickUp.lastButtonPress) > InputConstants.MENU_ACTION_DELAY && (Input.GetKey(KeyCode.E) || theController.isButtonPressed(theController.STATE_OPTION1))) {
			hasPassanger = false;
            GameNotification.HideNotification(dropOffText);

            GameObject newHuman = Instantiate(human);
            newHuman.transform.position = PassengerSystem.NextPassengerDestination;
            newHuman.transform.rotation = Quaternion.Euler(0,0,0);

            PickUp.lastButtonPress = Time.time;

            PassengerSystem.PassengerIndex++;
            NavigationHandler.targetPosition = PassengerSystem.NextPassengerPosition;
            

            CashEarnedHandler.notifyCashEarned((int)moneyToGive);
		}
	}
}
