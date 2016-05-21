using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundController : MonoBehaviour {
    // Sound buffers
    private static List<AudioSource> backgroundSounds = new List<AudioSource>();
    private static List<AudioSource> soundEffects = new List<AudioSource>();

    // Sound volumes
    private static float masterVolume = PlayerPrefs.GetFloat(GamePreferences.MASTER_VOLUME, GamePreferences.DEFAULT_MASTER_VOLUME);
    private static float backgroundMusicVolume = PlayerPrefs.GetFloat(GamePreferences.BG_VOLUME, GamePreferences.DEFAULT_BG_VOLUME);
    private static float soundEffectsVolume = PlayerPrefs.GetFloat(GamePreferences.SFX_VOLUME, GamePreferences.DEFAULT_SFX_VOLUME);

    void Start()
    {
        ClearBuffers();
    }

    // sound volume propertiers
    public static float MasterVolume
    {
        get { return masterVolume; }
    }

    public static float BackgroundMusicVolume
    {
        get { return backgroundMusicVolume; }
    }

    public static float SoundEffectsVolume
    {
        get { return soundEffectsVolume; }
    }

    /// <summary>
    /// This method will clear the underlying static soundEffects and backgroundMusic list buffers.</br>
    /// This should be called everytime a new scene is loaded, to get rid of previous sounds that might not be used in a new scene.
    /// </summary>
    public static void ClearBuffers()
    {
        backgroundSounds.Clear();
        soundEffects.Clear();
    }


    public static void SetMasterVolume(float vol)
    {
        masterVolume = vol;
        AudioListener.volume = masterVolume;
    }

    public static void SetBackgroundMusicVolume(float vol)
    {
        backgroundMusicVolume = vol;
        foreach (AudioSource a in backgroundSounds)
            if(a != null) a.volume = backgroundMusicVolume;
    }

    public static void SetSoundEffectVolume(float vol)
    {
        soundEffectsVolume = vol;
        foreach (AudioSource a in soundEffects)
            if(a != null) a.volume = soundEffectsVolume;
    }

    /// <summary>
    /// Generates an audio source for the given arguments, and returns it.<br/>
    /// This will also add the audio source to the underlying buffer list of background sounds.
    /// </summary>
    /// <param name="parentObject">The object that should have the audio attached</param>
    /// <param name="clip">The AudioClip to use</param>
    /// <param name="loop">Loop on play</param>
    /// <param name="playAwake">Play on awake</param>
    /// <returns>The generated audiosource</returns>
    public static AudioSource GenerateBackgroundSound(GameObject parentObject, AudioClip clip, bool loop, bool playAwake)
    {
        AudioSource audioSource = parentObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.playOnAwake = playAwake;
        audioSource.volume = backgroundMusicVolume;

        backgroundSounds.Add(audioSource);

        return audioSource;
    }

    /// <summary>
    /// Generates an audio source for the given arguments, and returns it.<br/>
    /// This will also add the audio source to the underlying buffer list of effect sounds.
    /// </summary>
    /// <param name="parentObject">The object that should have the audio attached</param>
    /// <param name="clip">The AudioClip to use</param>
    /// <param name="loop">Loop on play</param>
    /// <param name="playAwake">Play on awake</param>
    /// <returns>The generated audiosource</returns>
    public static AudioSource GenerateSoundEffect(GameObject parentObject, AudioClip clip, bool loop, bool playAwake)
    {
        AudioSource audioSource = parentObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.playOnAwake = playAwake;
        audioSource.volume = soundEffectsVolume;

        soundEffects.Add(audioSource);

        return audioSource;
    }
}
