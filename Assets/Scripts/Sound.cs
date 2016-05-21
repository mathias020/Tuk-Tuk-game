using UnityEngine;
using System.Collections.Generic;

public class Sound : MonoBehaviour {

	public AudioClip idle;
	public AudioClip accelerate;
    public AudioClip reverse;
    public AudioClip startUp;

    private AudioSource idle_src;
    private AudioSource accelerate_src;
    private AudioSource reverse_src;
    private AudioSource startUp_src;

    private bool isStarted = false;

	void Start()
	{
        idle_src = SoundController.GenerateSoundEffect(gameObject, idle, true, true);
        accelerate_src = SoundController.GenerateSoundEffect(gameObject, accelerate, true, false);
        reverse_src = SoundController.GenerateSoundEffect(gameObject, reverse, true, false);
        startUp_src = SoundController.GenerateSoundEffect(gameObject, startUp, false, false);
    }

    private void startIdle()
    {
        idle_src.Play();
        if (!accelerate_src.isPlaying)
            accelerate_src.Play();
        if (!reverse_src.isPlaying)
            reverse_src.Play();

        idle_src.volume = SoundController.SoundEffectsVolume;
        accelerate_src.volume = 0.0f;
        reverse_src.volume = 0.0f;
    }

	void Update(){
        if(theController.getState(theController.STATE_START) == false && isStarted)
        {
            isStarted = false;

            if (idle_src.isPlaying)
                idle_src.Stop();

            if(accelerate_src.isPlaying)
                accelerate_src.Stop();

            if (reverse_src.isPlaying)
                reverse_src.Stop();
        }
        if(theController.getState(theController.STATE_START) == true && !isStarted)
        {
            isStarted = true;
            startUp_src.Play();

            Invoke("startIdle", 0.65f);

        }
        else if(theController.getState(theController.STATE_START) && isStarted)
        { 
            if (Input.GetKey(KeyCode.W) || (theController.getState(theController.STATE_FORWARD) && CarNav.currentSpeed > 0))
            {
                accelerate_src.volume = Mathf.Lerp(accelerate_src.volume, SoundController.SoundEffectsVolume, 7f * Time.deltaTime);
                reverse_src.volume = Mathf.Lerp(reverse_src.volume, 0.0f, 7f * Time.deltaTime);
                idle_src.volume = Mathf.Lerp(idle_src.volume, 0.0f, 7f * Time.deltaTime);
		    }
            else if(Input.GetKey(KeyCode.S) || (theController.getState(theController.STATE_REVERSE) && CarNav.currentSpeed < 0))
            {
                accelerate_src.volume = Mathf.Lerp(accelerate_src.volume, 0.0f, 7f * Time.deltaTime);
                reverse_src.volume = Mathf.Lerp(reverse_src.volume, SoundController.SoundEffectsVolume, 7f * Time.deltaTime);
                idle_src.volume = Mathf.Lerp(idle_src.volume, 0.0f, 7f * Time.deltaTime);
            }
		    else
            {
                accelerate_src.volume = Mathf.Lerp(accelerate_src.volume, 0.0f, 7f * Time.deltaTime);
                reverse_src.volume = Mathf.Lerp(reverse_src.volume, 0.0f, 7f * Time.deltaTime);
                idle_src.volume = Mathf.Lerp(idle_src.volume, SoundController.SoundEffectsVolume, 7f * Time.deltaTime);
            }
        }

	}
}
