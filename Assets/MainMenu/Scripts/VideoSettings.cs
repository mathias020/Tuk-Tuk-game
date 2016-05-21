using UnityEngine;
using System.Collections;

public class VideoSettings : MonoBehaviour {

    public UI_Roulette screenResolution;
    public UI_Roulette fullscreen;
    public UI_Roulette vsync;

	void Start () {
        screenResolution.Value = PlayerPrefs.GetInt(GamePreferences.SCREEN_WIDTH, Screen.width) + "x" + PlayerPrefs.GetInt(GamePreferences.SCREEN_HEIGHT, Screen.height);
        fullscreen.Value = (PlayerPrefs.GetInt(GamePreferences.FULLSCREEN, (Screen.fullScreen ? 1 : 0)) == 1 ? "On" : "Off");
        vsync.Value = (PlayerPrefs.GetInt(GamePreferences.VSYNC, (QualitySettings.vSyncCount == 1 ? 1 : 0)) == 1 ? "On" : "Off");
    }
}
