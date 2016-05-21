using UnityEngine;
using System.Collections;

public class InputConstants {
    private InputConstants() { }
    // Menu control
    public const float      MENU_ADJUSTMENT_THRESHOLD_RIGHT      =           0.5f;
    public const float      MENU_ADJUSTMENT_THRESHOLD_LEFT       =           -0.5f;

    public const float      MENU_STEARING_THRESHOLD_LEFT         =           -0.8f;
    public const float      MENU_STEARING_THRESHOLD_RIGHT        =           0.8f;

    public const float      MENU_ACTION_DELAY                    =           0.8f;


    // Car Navigation & Stearing
    public const float      CARNAV_ACCELERATION_THRESHOLD        =           0.99f;

    public const float      CARNAV_STEARING_THRESHOLD_LEFT       =           -0.4f;
    public const float      CARNAV_STEARING_THRESHOLD_RIGHT      =           0.4f;

    public const float      CARNAV_FRICTION_THRESHOLD            =           0.04f;
    public const float      CARNAV_FRICTION_THRESHOLD_NEGATIVE   =          -0.04f;
    public const float      CARNAV_FRICTION_THRESHOLD_ZERO       =          0f;

    public const float      CARNAV_STEARING_DELAY                =           0.01f;

    public const int        CARNAV_EXPONENTIAL_INCLINE           =           2;

}
