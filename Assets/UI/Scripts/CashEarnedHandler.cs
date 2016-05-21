using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class CashEarnedHandler : MonoBehaviour {

    private static GameObject notifyCanvasEarned;
    private static GameObject notifyCanvasLost;

    private static Text cashEarned;
    private static Text cashValueEarned;

    private static Text cashLost;
    private static Text cashValueLost;

    private static CashEarnedHandler instance;

	// Use this for initialization
	void Start () {
        instance = this;
        notifyCanvasEarned = gameObject;
        notifyCanvasEarned.GetComponent<CanvasRenderer>().SetAlpha(0.0f);

        notifyCanvasLost = GameObject.Find("ui_notify_cash_out");
        notifyCanvasLost.GetComponent<CanvasRenderer>().SetAlpha(0.0f);

        cashEarned = GameObject.Find("ui_notify_cashearned").GetComponent<Text>();
        cashEarned.enabled = false;

        cashValueEarned = GameObject.Find("ui_notify_value").GetComponent<Text>();
        cashValueEarned.enabled = false;

        cashLost = GameObject.Find("ui_notify_cashlost").GetComponent<Text>();
        cashLost.enabled = false;

        cashValueLost = GameObject.Find("ui_notify_value_lost").GetComponent<Text>();
        cashValueLost.enabled = false;
    }

    public static void notifyCashLost(int amountLost)
    {
        cashValueLost.enabled = true;
        cashValueLost.text = "-$" + amountLost;
        cashLost.enabled = true;
        notifyCanvasLost.GetComponent<CanvasRenderer>().SetAlpha(1.0f);

        PointSystem.SubtractFromScore(amountLost);

        if (instance)
            instance.Invoke("hideNotificationLost", 1.25f);
        else
            Debug.Log("No instance found");
    }

    public static void notifyCashEarned(int amountEarned)
    {
        cashValueEarned.enabled = true;
        cashValueEarned.text = "+$" + amountEarned;
        cashEarned.enabled = true;
        notifyCanvasEarned.GetComponent<CanvasRenderer>().SetAlpha(1.0f);

        PointSystem.AddToScore(amountEarned);

        if (instance)
            instance.Invoke("hideNotification", 1.25f);
        else
            Debug.Log("No instance found");
    }

    private void hideNotification()
    {
        cashValueEarned.enabled = false;
        cashEarned.enabled = false;
        notifyCanvasEarned.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }

    private void hideNotificationLost()
    {
        cashValueLost.enabled = false;
        cashLost.enabled = false;
        notifyCanvasLost.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }
}
