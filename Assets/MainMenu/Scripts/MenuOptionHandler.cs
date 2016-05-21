using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuOptionHandler : MonoBehaviour {

    private VerticalSettingsController audio_vController;
    private VerticalSettingsController graphics_vController;

    void Start()
    {
        audio_vController = GameObject.Find("Audio_VerticalSettingsController").GetComponent<VerticalSettingsController>();
        graphics_vController = GameObject.Find("Graphics_VerticalSettingsController").GetComponent<VerticalSettingsController>();
    }

    public bool performAction()
    {
        switch(gameObject.name)
        {
            case "opt_StartGame":
                LoadingOverlayHandler.LoadNewScene(1);
                return false;

            case "opt_Options":
                Animator anim = GameObject.Find("OptionsPane").GetComponent<Animator>();

                anim.SetBool("isVisible", true);
                return true;

            case "opt_Quit":
                Application.Quit();
                return false;

            default:
                Debug.Log("Button has no asigned action.");
                return false;
        }
    }

    public bool performCloseAction()
    {
        switch(gameObject.name)
        {
            case "opt_Options":
                Animator anim = GameObject.Find("OptionsPane").GetComponent<Animator>();

                anim.SetBool("isVisible", false);

                return true;

            default:
                Debug.Log("Button has no assigned close action");

                return false;
        }
    }
}
