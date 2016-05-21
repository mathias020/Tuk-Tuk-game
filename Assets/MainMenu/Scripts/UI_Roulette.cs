using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_Roulette : MonoBehaviour {

    [Serializable]
    public struct Option
    {
        public string name;
        public string value;
    }

    public Option[] options;

    private int currentPosition = 0;
    private Text valueDisplay;

    // Settings for start-up
    public bool listAvailableResolutions = false;
    

    public string Value
    {
        get { return options[currentPosition].value; }
        set
        {
            for (int i = 0; i < options.Length; i++)
                if (options[i].value.Equals(value)) currentPosition = i;
        }
    }

	// Use this for initialization
	void Start () {
        if (listAvailableResolutions)
            initAvailableResolutions();

        

        if (options != null)
        {
            valueDisplay = GetComponentInChildren<Text>();
            valueDisplay.text = options[currentPosition].name;
        }
	}

    public void moveLeft()
    {
        if (options == null || options.Length == 0) return;

        currentPosition--;
        if (currentPosition < 0) currentPosition = options.Length - 1;

        valueDisplay.text = options[currentPosition].name;
    }

    public void moveRight()
    {
        if (options == null || options.Length == 0) return;

        currentPosition++;
        if (currentPosition >= options.Length) currentPosition = 0;

        valueDisplay.text = options[currentPosition].name;
    }

    private void initAvailableResolutions()
    {
        options = new Option[Screen.resolutions.Length];

        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            Option o = new Option();
            o.name = Screen.resolutions[i].width + "x" + Screen.resolutions[i].height;
            o.value = Screen.resolutions[i].width + "x" + Screen.resolutions[i].height;
            if (Screen.currentResolution.Equals(Screen.resolutions[i]))
                currentPosition = i;

            options[i] = o;
        }
    }
}
