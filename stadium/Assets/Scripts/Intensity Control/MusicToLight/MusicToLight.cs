using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToLight : MonoBehaviour
{
    public AudioSource audioSource;
    bool musicIsPlaying = false;

    void Start()
    {
        audioSource.Stop();
    }

    void Update()
    {
        // Debug.Log(audioSource.time);
    }

    public void OnPlayMusic()
    {
        if (musicIsPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
        musicIsPlaying = !musicIsPlaying;
    }

    public void OnPauseMusic()
    {
        if (musicIsPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
        musicIsPlaying = !musicIsPlaying;
    }
}
