using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameNotification : MonoBehaviour {

    private static Text notification_text;

    public float startAlpha = 0f;
    public float endAlpha = 1.0f;
    public float fadeStartAfter = 0.1f;
    public float fadeDuration = 1.0f;

    private const string COLOR_KEY = "#ff0000";

    public static string Message
    {
        set {
            
            value = value.Replace("<key>", "<color="+COLOR_KEY+">");
            value = value.Replace("</key>", "</color>");

            notification_text.text = value;
        }
    }

    public static void Reset()
    {
        notification_text.text = " ";
    }

	void Start () {
        notification_text = GetComponent<Text>();

        notification_text.GetComponent<CanvasRenderer>().SetAlpha(startAlpha);

        Invoke("fadeStageOne", fadeStartAfter);
    }

    private void fadeStageOne()
    {
        if (!notification_text) return;

        notification_text.CrossFadeAlpha(endAlpha, fadeDuration, false);

        Invoke("fadeStageTwo", fadeStartAfter);
    }

    private void fadeStageTwo()
    {
        if (!notification_text) return;

        notification_text.CrossFadeAlpha(startAlpha, fadeDuration, false);
        Invoke("fadeStageOne", fadeStartAfter);
    }

    public static void ShowNotification()
    {
        notification_text.enabled = true;
    }

    public static void HideNotification(string msg)
    {
        msg = msg.Replace("<key>", "<color=" + COLOR_KEY + ">");
        msg = msg.Replace("</key>", "</color>");
        if (notification_text.text.Equals(msg))
            notification_text.enabled = false;
    }
}
