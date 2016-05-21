using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GearDisplay : MonoBehaviour {

    private Text gear_value;
    private Animator anim;

    private const string NEUTRAL_COLOR = "#A69B00";
    private const string REVERSE_COLOR = "#B20000";
    private const string FORWARD_COLOR = "#037000";

	// Use this for initialization
	void Start () {
        gear_value = GetComponent<Text>();
        anim = GetComponent<Animator>();
	}
	
    private void setValue(string gearVal, string color)
    {
        if (!gearVal.Equals("N"))
            gear_value.text = "<color=" + color + ">" + gearVal + "</color>";
        else
            gear_value.text = gearVal;

        if (!gearVal.Equals("N"))
        {
            anim.enabled = false;
        } else
        {
            anim.enabled = true;
            anim.Play(Animator.StringToHash("flashing_gear"));
        }
            
    }

    void Update()
    {
        if(theController.getState(theController.STATE_NEUTRAL))
        {
            setValue("N", NEUTRAL_COLOR);
        }
        else if (theController.getState(theController.STATE_REVERSE))
        {
            setValue("R", REVERSE_COLOR);
        }
        else if (theController.getState(theController.STATE_FORWARD))
        {
            setValue("F", FORWARD_COLOR);
        } else
        {
            setValue("N", NEUTRAL_COLOR);
        }
    }
}
