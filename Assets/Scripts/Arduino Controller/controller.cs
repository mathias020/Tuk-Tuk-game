using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class controller : MonoBehaviour {
    public static readonly string BUTTON_LEFT = "BUTTON_LEFT";
    public static readonly string BUTTON_RIGHT = "BUTTON_RIGHT";
    public static readonly string BUTTON_UP = "BUTTON_UP";
    public static readonly string BUTTON_DOWN = "BUTTON_DOWN";
    public static readonly string BUTTON_ENTER = "BUTTON_ENTER";

    private static bool isButtonLeftPressed;
    private static bool isButtonRightPressed;
    private static bool isButtonUpPressed;
    private static bool isButtonDownPressed;
    private static bool isButtonEnterPressed;

    SerialPort sp = new SerialPort("COM3", 9600);

	// Use this for initialization
	void Start () {
        sp.ReadTimeout = SerialPort.InfiniteTimeout;
        sp.Open();

        isButtonLeftPressed = false;
        isButtonRightPressed = false;
        isButtonUpPressed = false;
        isButtonDownPressed = false;
        isButtonEnterPressed = false;
    }
	
	// Update is called once per frame
	void Update () {
        readSerialPort();
	}

    //reads Serial Port
    public void readSerialPort()
    {
        if (sp.IsOpen)
        {
            try
            {
                string readLine = sp.ReadLine();
                Debug.Log("Read byte: " + readLine);

                processReadLine(readLine);
            }
            catch (System.Exception)
            {
                Debug.Log("Exception caught");
                throw;
            }
        }
    }

    private void processReadLine(string readLine)
    {
        char[] segments = readLine.ToCharArray();

        isButtonLeftPressed = segments[0] == '1' ? true : false;
        isButtonUpPressed = segments[1] == '1' ? true : false;
        isButtonDownPressed = segments[2] == '1' ? true : false;
        isButtonRightPressed = segments[3] == '1' ? true : false;
        isButtonEnterPressed = segments[4] == '1' ? true : false;

    }

    public static bool isButtonPressed(string button)
    {
       switch (button)
        {
            case "BUTTON_LEFT":
                //Debug.Log("BUTTON_LEFT: " + isButtonLeftPressed);
                return isButtonLeftPressed;
            case "BUTTON_RIGHT":
                //Debug.Log("BUTTON_RIGHT: " + isButtonRightPressed);
                return isButtonRightPressed;
            case "BUTTON_UP":
                //Debug.Log("BUTTON_UP: " + isButtonUpPressed);
                return isButtonUpPressed;
            case "BUTTON_DOWN":
                //Debug.Log("BUTTON_DOWN: " + isButtonDownPressed);
                return isButtonDownPressed;
            case "BUTTON_ENTER":
                //Debug.Log("BUTTON_ENTER: " + isButtonEnterPressed);
                return isButtonEnterPressed;
            default:
                return false;
        }
    }
}
