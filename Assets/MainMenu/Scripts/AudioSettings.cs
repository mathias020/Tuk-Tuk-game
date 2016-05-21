using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour {

    public Slider masterVolume;
    public Slider backgroundVolume;
    public Slider sfxVolume;

    void Start()
    {
        masterVolume.value = SoundController.MasterVolume;
        backgroundVolume.value = SoundController.BackgroundMusicVolume;
        sfxVolume.value = SoundController.SoundEffectsVolume;

        Debug.Log("MV = " + masterVolume.value + "\nBV = " + backgroundVolume.value + "\nSFXV = " + sfxVolume.value);
    }

    public void updateMasterVolume()
    {
        SoundController.SetMasterVolume(masterVolume.value);
    }

    public void updateBackgroundVolume()
    {
        SoundController.SetBackgroundMusicVolume(backgroundVolume.value);
    }

    public void updateSoundEffectVolume()
    {
        SoundController.SetSoundEffectVolume(sfxVolume.value);
    }
}
