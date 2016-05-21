using UnityEngine;
using System.Collections;

public class BackgroundSound : MonoBehaviour {

    public AudioClip soundClip;

    public bool playOnStart = true;

    private AudioSource soundClip_src;

	void Start () {
        soundClip_src = SoundController.GenerateBackgroundSound(gameObject, soundClip, true, false);

        if (playOnStart)
            soundClip_src.Play();
	}

    public void Play()
    {
        if(!soundClip_src.isPlaying)
            soundClip_src.Play();
    }

    public void Stop()
    {
        if (soundClip_src.isPlaying)
            soundClip_src.Stop();
    }
}
