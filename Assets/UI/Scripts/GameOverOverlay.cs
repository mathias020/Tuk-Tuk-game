using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverOverlay : MonoBehaviour {

    private static Text finalScore;
    private static Text goToMenu;
    private static Text gameOver_label;
    private static Text yourScore_label;
    private static GameOverOverlay instance;

    private static bool gameIsOver = false;

    public static bool GameIsOver
    {
        get { return gameIsOver;  }
        set
        {
            instance.toggleOverlay(value);
            if (value)
            {
                
                instance.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
                goToMenu.GetComponent<CanvasRenderer>().SetAlpha(instance.startAlpha);
                instance.Invoke("flashMenuMessageOne", instance.fadeStartAfter);
                gameIsOver = true;
            }
        }
    }

    public float startAlpha = 0f;
    public float endAlpha = 1.0f;
    public float fadeStartAfter = 0.1f;
    public float fadeDuration = 1.0f;

    private static int final_score = 0;

    public static int Score {
        set {
            if(value < 0)
            {
                finalScore.color = Color.red;
                finalScore.text = "$" + value;
            }  else
            {
                finalScore.text = "$" + value;
                finalScore.color = Color.green;
            }
        }
    }

	void Start () {
        instance = this;
        finalScore = GameObject.Find("finalScore_label").GetComponent<Text>();
        goToMenu = GameObject.Find("goToMenu_label").GetComponent<Text>();
        gameOver_label = GameObject.Find("gameOver_label").GetComponent<Text>();
        yourScore_label = GameObject.Find("yourScore_label").GetComponent<Text>();

        toggleOverlay(false);
    }

    private void toggleOverlay(bool toggle)
    {
        GetComponent<RawImage>().enabled = toggle;
        finalScore.enabled = toggle;
        goToMenu.enabled = toggle;
        gameOver_label.enabled = toggle;
        yourScore_label.enabled = toggle;
    }

    private void flashMenuMessageOne()
    {
        if (!goToMenu) return;

        goToMenu.CrossFadeAlpha(endAlpha, fadeDuration, false);

        Invoke("flashMenuMessageTwo", fadeStartAfter);
    }

    private void flashMenuMessageTwo()
    {
        if (!goToMenu) return;

        goToMenu.CrossFadeAlpha(startAlpha, fadeDuration, false);
        Invoke("flashMenuMessageOne", fadeStartAfter);
    }
}
