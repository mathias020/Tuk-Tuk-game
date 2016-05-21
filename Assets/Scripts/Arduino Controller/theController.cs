using UnityEngine;
using System.IO.Ports;
using System;
using System.Threading;

public class theController : MonoBehaviour
{
    public static readonly string STATE_START = "STATE_START";
    public static readonly string STATE_REVERSE = "STATE_REVERSE";
    public static readonly string STATE_NEUTRAL = "STATE_NEUTRAL";
    public static readonly string STATE_FORWARD = "STATE_FORWARD";
    public static readonly string STATE_OPTION1 = "STATE_OPTION1";
    public static readonly string STATE_OPTION2 = "STATE_OPTION2";
    public static readonly string STATE_OPTION3 = "STATE_OPTION3";
    public static readonly string STATE_BREAK = "STATE_BREAK";

    private static bool stateStart;
    private static float stearing;
    private static float acceleration;
    private static int gear;
    private static bool stateReverse;
    private static bool stateNeutral;
    private static bool stateForward;
    private static bool stateOption1;   //switch
    private static bool stateOption2;   //switch
    private static bool stateOption3;   //switch
    private static bool stateBreak;

    private static bool isRunning = true;

    private bool previousStateOption1;
    private bool previousStateOption2;
    private bool previousStateOption3;

    SerialPort sp = new SerialPort("COM4", 9600);

    private Thread arduinoReader;

    // Use this for initialization
    void Start()
    {
        sp.ReadTimeout = SerialPort.InfiniteTimeout;
        sp.Open();

        try
        {
            sp.ReadLine();
            sp.ReadLine();
        } catch (Exception)
        {
            ;
        }

        //start values
        stateStart = false;
        stearing = 0.0f;
        acceleration = 0.0f;
        gear = 0;
        stateReverse = false;
        stateNeutral = true;
        stateForward = false;
        stateOption1 = false;
        stateOption2 = false;
        stateOption3 = false;
        stateBreak = false;

        previousStateOption1 = true;
        previousStateOption2 = true;
        previousStateOption3 = true;

        arduinoReader = new Thread(() => {
            Debug.Log("A new arduino reader thread as been created.");
            while (isRunning)
            {
                if(sp.IsOpen)
                {
                    try
                    {
                        string readLine = sp.ReadLine();
                        Debug.Log("Read line: " + readLine);

                        processReadLine(readLine);

                        Debug.Log("Forward: " + stateForward + "\nNeutral: " + stateNeutral + "\nReverse: " + stateReverse);
                        
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

            }
            Debug.Log("Arduino Thread closed");
        });
        arduinoReader.Start();
    }

    void OnDestroy()
    {
        isRunning = false;
        if (sp.IsOpen)
        {
            sp.Close();
            Debug.Log("SerialPort closed.");
        }
        
        Debug.Log("OnDestroy called for Controller");
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void processReadLine(string readLine)
    {
        char[] delimiterChars = { ' ' };
        string[] words = readLine.Split(delimiterChars);
        bool temp;  
              
        stateStart = (words[0] == "1") ? true : false;
        stearing = Convert.ToSingle(words[1]);
        acceleration = Convert.ToSingle(words[2]);
        gear = Convert.ToInt32(words[3]);
        
        stateForward = (words[4] == "1") ? true : false;
        stateNeutral = (words[5] == "1") ? true : false;
        stateReverse = (words[6] == "1") ? true : false;
        
        temp = (words[7] == "1") ? true : false;
        stateOption1 = changeStateIfOption1(temp);             

        temp = (words[8] == "1") ? true : false;
        stateOption2 = changeStateIfOption2(temp);

        temp = (words[9] == "1") ? true : false;
        stateOption3 = changeStateIfOption3(temp);

        stateBreak = (words[10] == "1") ? true : false;

        /*Debug.Log("stateStart: " + stateStart 
                  + "stearing: " + stearing 
                  + "acceleration: " + acceleration 
                  + "gear: " + gear
                  + "stateReverse: " + stateReverse
                  + "stateNeutral: " + stateNeutral
                  + "stateForward: " + stateForward
                  + "stateOption1: " + stateOption1
                  + "stateOption2: " + stateOption2
                  + "stateOption3: " + stateOption3
                  + "stateBreak: " + stateBreak);*/
    }

    private bool changeStateIfOption1(bool newState)
    {
        if (previousStateOption1 != newState)
        {
            previousStateOption1 = newState;
            return true;
        }
        else
            return false;
    }
    private bool changeStateIfOption2(bool newState)
    {
        if (previousStateOption2 != newState)
        {
            previousStateOption2 = newState;
            return true;
        }
        else
            return false;
    }
    private bool changeStateIfOption3(bool newState)
    {
        if (previousStateOption3 != newState)
        {
            previousStateOption3 = newState;
            return true;
        }
        else
            return false;
    }

    public static bool isButtonPressed(string button)
    {
        switch (button)
        {
            case "STATE_OPTION1":
                //Debug.Log("STATE_OPTION1: " + stateOption1);
                return stateOption1;
            case "STATE_OPTION2":
                //Debug.Log("STATE_OPTION2: " + stateOption2);
                return stateOption2;
            case "STATE_OPTION3":
                //Debug.Log("STATE_OPTION3: " + stateOption3);
                return stateOption3;            
            default:
                return false;
        }
    }

    public static bool getState(string state)
    {
        switch(state)
        {
            case "STATE_REVERSE":
                //Debug.Log("STATE_REVERSE: " + stateReverse);                
                return stateReverse;
            case "STATE_NEUTRAL":
                //Debug.Log("STATE_NEUTRAL: " + stateNeutral);
                return stateNeutral;
            case "STATE_FORWARD":
                //Debug.Log("STATE_FORWARD: " + stateForward);
                return stateForward;
            case "STATE_START":
                //Debug.Log("STATE_START: " + stateStart);
                return stateStart;
                
            case "STATE_BREAK":
                //Debug.Log("STATE_BREAK: " + stateBreak);
                return stateBreak;
            default:
                return false;
        }
    }
    public static float getStearing()
    {
        return stearing;
    }
    public static float getAcceleration()
    {
        return acceleration;
    }
    public static int getGear()
    {
        return gear;
    }
}
