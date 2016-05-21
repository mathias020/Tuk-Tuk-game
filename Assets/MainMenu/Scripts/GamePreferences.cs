using UnityEngine;
using System.Collections;

public class GamePreferences : MonoBehaviour {
    
    private GamePreferences() {}

    public const float DEFAULT_MASTER_VOLUME    =           0.5f;
    public const float DEFAULT_BG_VOLUME        =           0.5f;
    public const float DEFAULT_SFX_VOLUME       =           0.5f;

    public const string SCREEN_WIDTH            =           "screen_width";
    public const string SCREEN_HEIGHT           =           "screen_height";

    public const string FULLSCREEN              =           "fullscreen";
    public const string VSYNC                   =           "vsync";

    public const string MASTER_VOLUME           =           "master_volume";
    public const string BG_VOLUME               =           "bg_volume";
    public const string SFX_VOLUME              =           "sfx_volume";
}
