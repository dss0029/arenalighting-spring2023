using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MusicSynchronizerModeController : MonoBehaviour
{
    [SerializeField] UnityEvent<MusicSynchronizerMode> syncronizerModeChangeEvent;
    public TMP_Text musicSynchronizerModeText;
    private int currentSelectedMode = 0;
    private List<MusicSynchronizerMode> musicSynchronizerModes = new List<MusicSynchronizerMode>();

    void Start()
    {
        foreach (MusicSynchronizerMode mode in Enum.GetValues(typeof(MusicSynchronizerMode)))
        {
            musicSynchronizerModes.Add(mode);
        }

        musicSynchronizerModeText.text = musicSynchronizerModes[currentSelectedMode].ToString();
    }

    public void ControlTypeLeft()
    {
        if (currentSelectedMode == 0)
        {
            currentSelectedMode = musicSynchronizerModes.Count - 1;
        }
        else
        {
            currentSelectedMode -= 1;
        }

        ProcessModeUpdate();
    }

    public void ControlTypeRight()
    {
        if (currentSelectedMode == musicSynchronizerModes.Count - 1)
        {
            currentSelectedMode = 0;
        }
        else
        {
            currentSelectedMode += 1;
        }

        ProcessModeUpdate();
    }

    void ProcessModeUpdate()
    {
        musicSynchronizerModeText.text = musicSynchronizerModes[currentSelectedMode].ToString();
        syncronizerModeChangeEvent.Invoke(musicSynchronizerModes[currentSelectedMode]);
    }
}
