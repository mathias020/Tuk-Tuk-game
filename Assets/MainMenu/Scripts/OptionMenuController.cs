using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour {
    public GameObject[] menuItems;

    private static int currentPosition = 0;

    public static int CurrentPosition
    {
        get { return currentPosition;  }
    }

    public AudioClip menuSelect;

    private AudioSource menuSelect_src;

    private float lastMenuMove;

    private MenuController mController;
    private VerticalSettingsController audio_vController;
    private VerticalSettingsController graphics_vController;

    // Use this for initialization
    void Start () {
        mController = GameObject.Find("MenuController").GetComponent<MenuController>();
        audio_vController = GameObject.Find("Audio_VerticalSettingsController").GetComponent<VerticalSettingsController>();
        graphics_vController = GameObject.Find("Graphics_VerticalSettingsController").GetComponent<VerticalSettingsController>();
        menuSelect_src = SoundController.GenerateSoundEffect(gameObject, menuSelect, false, false);
	}

    public int GetOptionPosition(string name)
    {
        if (menuItems == null) return -1;

        for (int i = 0; i < menuItems.Length; i++)
            if (menuItems[i].name.Equals(name))
                return i;

        return -1;
    }

    // Update is called once per frame
    void Update () {
        if (audio_vController.SelectedPosition == -1 && graphics_vController.SelectedPosition == -1 && MenuController.activeMenu == mController.GetOptionPosition("opt_Options") && (Time.time - lastMenuMove) > InputConstants.MENU_ACTION_DELAY)
        {
            if (theController.getStearing() < InputConstants.MENU_STEARING_THRESHOLD_LEFT || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lastMenuMove = Time.time;
                menuLeft();
            }
            if (theController.getStearing() > InputConstants.MENU_STEARING_THRESHOLD_RIGHT || Input.GetKeyDown(KeyCode.RightArrow))
            {
                lastMenuMove = Time.time;
                menuRight();
            }
        }
    }

    private void setSelectedOn(int position, bool toggle)
    {
        GameObject gObj = menuItems[position];
        RawImage ri = gObj.GetComponentInChildren<RawImage>();
        ri.enabled = toggle;

        GameObject pane = GameObject.Find(gObj.name + "_Pane");
        
        if (pane != null)
        {
            Canvas c = pane.GetComponent<Canvas>();
            c.enabled = toggle;
        }

        Text txt = gObj.GetComponentInChildren<Text>();
        txt.color = toggle ? Color.black : Color.white;
    }

    private void menuLeft()
    {
        menuSelect_src.Play();
        setSelectedOn(currentPosition, false);

        currentPosition--;
        if (currentPosition < 0) currentPosition = menuItems.Length - 1;

        setSelectedOn(currentPosition, true);

    }

    private void menuRight()
    {
        menuSelect_src.Play();
        setSelectedOn(currentPosition, false);

        currentPosition++;
        if (currentPosition >= menuItems.Length) currentPosition = 0;

        setSelectedOn(currentPosition, true);

    }
}
