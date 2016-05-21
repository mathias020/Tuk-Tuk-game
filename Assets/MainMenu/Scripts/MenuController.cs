using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public GameObject[] menuOptions;

    private int currentPosition = 0;

    public AudioClip menuSelect;
    public AudioClip menuEnter;

    private AudioSource menuSelect_src;
    private AudioSource menuEnter_src;

    private float lastMenuMove;

    public static int activeMenu = -1; // -1 indicating no menu has been opened

    private VerticalSettingsController audio_vController;
    private VerticalSettingsController graphics_vController;

    void Start()
    {
        setAnimationOn(currentPosition, true);

        audio_vController = GameObject.Find("Audio_VerticalSettingsController").GetComponent<VerticalSettingsController>();
        graphics_vController = GameObject.Find("Graphics_VerticalSettingsController").GetComponent<VerticalSettingsController>();
        menuSelect_src = SoundController.GenerateSoundEffect(gameObject, menuSelect, false, false);
        menuEnter_src = SoundController.GenerateSoundEffect(gameObject, menuEnter, false, false);
    }

    public int GetOptionPosition(string name)
    {
        if (menuOptions == null) return -1;

        for (int i = 0; i < menuOptions.Length; i++)
            if (menuOptions[i].name.Equals(name))
                return i;

        return -1;
    }

    void Update()
    {
        if(!LoadingOverlayHandler.IsLoading && activeMenu == -1 && (Time.time - lastMenuMove) > InputConstants.MENU_ACTION_DELAY)
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
            if (theController.isButtonPressed(theController.STATE_OPTION1) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                lastMenuMove = Time.time;
                menuChoose();
            }
        }

        if(activeMenu != -1 && audio_vController.SelectedPosition == -1 && graphics_vController.SelectedPosition == -1)
        {
            if (theController.isButtonPressed(theController.STATE_OPTION2) || Input.GetKeyDown(KeyCode.Escape))
            {
                menuClose();
            }
        }

    }

    private void menuClose()
    {
        menuEnter_src.Play();

        GameObject gObject = menuOptions[currentPosition];

        if (gObject != null)
        {
            MenuOptionHandler moh = gObject.GetComponent<MenuOptionHandler>();
            if(moh.performCloseAction())
                activeMenu = -1;
        }
    }

    private void menuChoose()
    {
        menuEnter_src.Play();

        GameObject gObject = menuOptions[currentPosition];

        if(gObject != null)
        {
            MenuOptionHandler moh = gObject.GetComponent<MenuOptionHandler>();
            if(moh.performAction())
                activeMenu = currentPosition;
        }
    }

    private void setAnimationOn(int menuIdx, bool toggle)
    {
        Animator anim;
        GameObject gObject;

        gObject = menuOptions[menuIdx];

        anim = gObject.GetComponent<Animator>();
        anim.SetBool("selected", toggle);
    }

    private void menuLeft()
    {
        menuSelect_src.Play();
        setAnimationOn(currentPosition, false);

        currentPosition--;
        if (currentPosition < 0) currentPosition = menuOptions.Length - 1;

        setAnimationOn(currentPosition, true);
       
    }

    private void menuRight()
    {
        menuSelect_src.Play();
        setAnimationOn(currentPosition, false);

        currentPosition++;
        if (currentPosition >= menuOptions.Length) currentPosition = 0;

        setAnimationOn(currentPosition, true);
        
    }
}
