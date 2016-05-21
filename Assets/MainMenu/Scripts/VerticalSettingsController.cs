using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VerticalSettingsController : MonoBehaviour {

    public RuntimeAnimatorController selectedAnimationController;

    [Serializable]
    public struct Option
    {
        public GameObject optionName;
        public GameObject optionControl;
    }

    public Option[] options;

    private int currentPosition = 0;

    public int CurrentPosition
    {
        get { return currentPosition;  }
        set {
            setSelectedOn(currentPosition, false);
            setSelectedOn(value, true);
            currentPosition = value;
        }
    }

    private int selectedPosition = -1;

    public int SelectedPosition
    {
        get { return selectedPosition;  }
    }

    private float lastMenuMove = 0;

    private MenuController mController;
    private OptionMenuController oController;

    public GameObject optionName;

    void Start()
    {
        mController = GameObject.Find("MenuController").GetComponent<MenuController>();
        oController = GameObject.Find("OptionMenuController").GetComponent<OptionMenuController>();

        setSelectedOn(currentPosition, true);
    }

	void Update () {
	    if(MenuController.activeMenu == mController.GetOptionPosition("opt_Options") && 
            OptionMenuController.CurrentPosition == oController.GetOptionPosition(optionName.name) && 
            (Time.time - lastMenuMove) > InputConstants.MENU_ACTION_DELAY)
        {
            if(selectedPosition == -1)
            {
                if(theController.isButtonPressed(theController.STATE_OPTION3) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    menuDown();
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (currentPosition == 0) return;
                    menuUp();
                }
                if(theController.isButtonPressed(theController.STATE_OPTION1) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                {
                    selectedPosition = currentPosition;

                    selectItem();
                }
            }
            else
            {
                if(theController.getStearing() < -0.5f || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Option o = options[selectedPosition];
                    if (o.optionControl.GetComponent<Slider>() != null)
                    {
                        Slider s = o.optionControl.GetComponent<Slider>();
                        s.value -= 0.01f;
                    }
                    if(o.optionControl.GetComponent<UI_Roulette>() != null)
                    {
                        UI_Roulette uir = o.optionControl.GetComponent<UI_Roulette>();
                        uir.moveLeft();
                    }
                }
                if (theController.getStearing() > 0.5f || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Option o = options[selectedPosition];
                    if (o.optionControl.GetComponent<Slider>() != null)
                    {
                        Slider s = o.optionControl.GetComponent<Slider>();
                        s.value += 0.01f;
                    }
                    if (o.optionControl.GetComponent<UI_Roulette>() != null)
                    {
                        UI_Roulette uir = o.optionControl.GetComponent<UI_Roulette>();
                        uir.moveRight();
                    }
                }

                if (theController.isButtonPressed(theController.STATE_OPTION2) || Input.GetKeyDown(KeyCode.Escape))
                {
                    Option o = options[selectedPosition];
                    if(o.optionName.name.Equals("MasterVolume_label"))
                    {
                        PlayerPrefs.SetFloat(GamePreferences.MASTER_VOLUME, SoundController.MasterVolume);
                    }
                    else if (o.optionName.name.Equals("BackgroundVolume_label"))
                    {
                        PlayerPrefs.SetFloat(GamePreferences.BG_VOLUME, SoundController.BackgroundMusicVolume);
                    }
                    else if (o.optionName.name.Equals("SoundEffects_label"))
                    {
                        PlayerPrefs.SetFloat(GamePreferences.SFX_VOLUME, SoundController.SoundEffectsVolume);
                    }
                    else if (o.optionName.name.Equals("Resolution_label"))
                    {
                        UI_Roulette uir = o.optionControl.GetComponent<UI_Roulette>();
                        if(uir != null)
                        {
                            string[] res = uir.Value.Split('x');
                            if(Screen.height != int.Parse(res[0]) && Screen.width != int.Parse(res[1]))
                            {
                                if(res.Length == 2)
                                {
                                    Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), true);
                                    PlayerPrefs.SetInt(GamePreferences.SCREEN_WIDTH, Screen.width);
                                    PlayerPrefs.SetInt(GamePreferences.SCREEN_HEIGHT, Screen.height);
                                }
                                    
                            }
                        }
                    }
                    else if(o.optionName.name.Equals("Fullscreen_label"))
                    {
                        UI_Roulette uir = o.optionControl.GetComponent<UI_Roulette>();
                        if(uir != null)
                        {
                            if (uir.Value.Equals("On"))
                                Screen.fullScreen = true;
                            else
                                Screen.fullScreen = false;

                            PlayerPrefs.SetInt(GamePreferences.FULLSCREEN, (uir.Value.Equals("On") ? 1 : 0) );
                        }
                    }
                    else if(o.optionName.name.Equals("VSync_label"))
                    {
                        UI_Roulette uir = o.optionControl.GetComponent<UI_Roulette>();
                        if (uir != null)
                        {
                            if (uir.Value.Equals("On"))
                                QualitySettings.vSyncCount = 1;
                            else
                                QualitySettings.vSyncCount = 0;

                            PlayerPrefs.SetInt(GamePreferences.VSYNC, (uir.Value.Equals("On") ? 1 : 0));
                        }
                    }

                    PlayerPrefs.Save();

                    deSelectItem();
                    Invoke("deselect", 0.1f);
                }
            }
        }
    }

    private void deselect()
    {
        selectedPosition = -1;
    }

    private void selectItem()
    {
        Option o = options[selectedPosition];
        if (o.optionControl.GetComponent<Slider>() != null)
        {
            EventSystem.current.SetSelectedGameObject(o.optionControl);
        }
        Animator a = o.optionName.AddComponent<Animator>();
        a.runtimeAnimatorController = selectedAnimationController;
        a.SetBool("selected", true);
    }

    private void deSelectItem()
    {
        Option o = options[selectedPosition];
        if (o.optionControl.GetComponent<Slider>() != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        Destroy(o.optionName.GetComponent<Animator>());
        setSelectedOn(currentPosition, true);
    }

    private void setSelectedOn(int position, bool toggle)
    {
        Option o = options[position];

        GameObject gobj = o.optionName;
        Text t = gobj.GetComponent<Text>();
        t.color = toggle ? new Color(255, 222, 0) : Color.white;
    }

    private void menuDown()
    {
        setSelectedOn(currentPosition, false);
        currentPosition++;
        if (currentPosition == options.Length) currentPosition = 0;
        setSelectedOn(currentPosition, true);
    }

    private void menuUp()
    {
        setSelectedOn(currentPosition, false);
        currentPosition--;
        setSelectedOn(currentPosition, true);
    }
}
