//This script allows you to toggle music to play and stop.
//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.

using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    private AudioSource[] m_MyAudioSource;
    public Slider slider;
    private bool isMusicPlaying = false;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        gameObject.tag = GameManager.AUDIO_TAG;
        m_MyAudioSource = gameObject.GetComponents<AudioSource>();
        // To play the background music
        PlayMusic();
        isMusicPlaying = true;
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(transform.gameObject);

    }

    // To enable or disable the audio
    public void SwitchAudioMode()
    {
        isMusicPlaying = !isMusicPlaying;
        if (isMusicPlaying)
        {
            PlayMusic();
        } else
        {
            StopMusic();
        }
    }

    // To play the background music
    public void PlayMusic()
    {
        isMusicPlaying = true;
        if (m_MyAudioSource[0].isPlaying) return;
        m_MyAudioSource[0].Play();
    }

    // To stop the background music
    public void StopMusic()
    {
        isMusicPlaying = false;
        m_MyAudioSource[0].Stop();
    }

    // When going back to the main menu, to reset the audio
    public void KillMusic()
    {
        StopMusic();
        Destroy(transform.gameObject);
    }

    // To set the volume from the main menu slider
    public void SetVolume(float volume)
    {
        foreach (AudioSource aud in m_MyAudioSource)
        {
            aud.volume = volume;
        }
    }

    // Functions to play different sounds during the game

    public void loosePointSound()
    {
        if (isMusicPlaying)
        {
            m_MyAudioSource[1].Play();
        }
    }

    public void wolfSound()
    {
        if (isMusicPlaying)
        {
            m_MyAudioSource[2].Play();
        }
    }

    public void sheepSound()
    {
        if (isMusicPlaying)
        {
            m_MyAudioSource[3].Play();
        }
    }

    public void winPointSound()
    {
        if (isMusicPlaying)
        {
            m_MyAudioSource[4].Play();
        }
    }

    public void gemAppearedSound()
    {
        if (isMusicPlaying)
        {
            m_MyAudioSource[5].Play();
        }
    }

    public void gemEarnedSound()
    {
        if (isMusicPlaying)
        {
            m_MyAudioSource[6].Play();
        }
    }

    public void powerOfGemUsedSound()
    {
        if (isMusicPlaying)
        {
            m_MyAudioSource[7].Play();
        }
    }
}