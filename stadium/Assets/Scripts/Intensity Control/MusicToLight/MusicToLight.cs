using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MusicToLight : MonoBehaviour
{
    public AudioPeer audioPeer;

    public float minScale = 0.25f;
    public float maxScale = 0.35f;
    public bool useBuffer = true;

    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject stopButton;
    public GameObject loadMusicButton;
    public GameObject musicToLightModeText;
    public MusicToLightModeController musicToLightModeController;

    private GameObject[] allLEDs;
    bool randomLight = false;

    private float musicVolume = 1.0f;

    void Start()
    {
        audioPeer.audioSource.playOnAwake = false;
        audioPeer.audioSource.Stop();
        allLEDs = GameObject.FindGameObjectsWithTag("LED");
    }

    void Update()
    {
        string currentMusicToLightMode = musicToLightModeController.getCurrentMode();
        if (currentMusicToLightMode == "Linear") {
            linearMode();
        }
        else if (currentMusicToLightMode == "Random")
        {
            randomMode();
        }
        else if (currentMusicToLightMode == "Amplitude")
        {
            amplitudeMode();
        }

        // Update volume
        audioPeer.audioSource.volume = musicVolume;
    }

    void linearMode()
    {
        if (audioPeer.audioSource.isPlaying)
        {
            for (int i = 0; i < allLEDs.Length; i++)
            {
                // Change transparency of the current led
                Color currentLedColor = allLEDs[i].GetComponent<Renderer>().material.color;
                Color newLedColor = new Color(currentLedColor.r, currentLedColor.g, currentLedColor.b, (audioPeer.audioBand[i % 8]));

                allLEDs[i].GetComponent<Renderer>().material.color = newLedColor;
                allLEDs[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", newLedColor);
            }
        }
    }

    void randomMode()
    {
        if (audioPeer.audioSource.isPlaying)
        {
            for (int i = 0; i < allLEDs.Length; i++)
            {
                int bandIndex = Random.Range(0, 8);

                // Change transparency of the current led
                Color currentLedColor = allLEDs[i].GetComponent<Renderer>().material.color;
                Color newLedColor = new Color(currentLedColor.r, currentLedColor.g, currentLedColor.b, (audioPeer.audioBand[bandIndex % 8]));

                allLEDs[i].GetComponent<Renderer>().material.color = newLedColor;
                allLEDs[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", newLedColor);
            }
        }
    }

    void amplitudeMode()
    {
        if (audioPeer.audioSource.isPlaying)
        {
            for (int i = 0; i < allLEDs.Length; i++)
            {
                // Change transparency of the current led
                Color currentLedColor = allLEDs[i].GetComponent<Renderer>().material.color;
                Color newLedColor = new Color(currentLedColor.r, currentLedColor.g, currentLedColor.b, (audioPeer.amplitudeBuffer));

                allLEDs[i].GetComponent<Renderer>().material.color = newLedColor;
                allLEDs[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", newLedColor);
            }
        }
    }

    public void OnPlayMusic()
    {
        playButton.SetActive(false);
        pauseButton.SetActive(true);

        if (audioPeer.audioSource.time > 0)
        {
            audioPeer.audioSource.UnPause();
            return;
        }

        audioPeer.audioSource.Play();
    }

    public void OnPauseMusic()
    {
        playButton.SetActive(true);
        pauseButton.SetActive(false);

        audioPeer.audioSource.Pause();
    }

    public void OnStopMusic()
    {
        playButton.SetActive(true);
        pauseButton.SetActive(false);

        audioPeer.audioSource.Stop();
        audioPeer.ResetValues();
    }

    public void OnLoadMusic()
    {
        string path = OpenFilePanel();
        if (path != null) {
            OnStopMusic();
            playButton.SetActive(false);
            stopButton.SetActive(false);
            loadMusicButton.GetComponentInChildren<TextMeshProUGUI>().text = "Loading...";
            loadMusicButton.GetComponent<Button>().interactable = false;
            StartCoroutine(GetAudioClip(path));
        }
        
    }

    string OpenFilePanel()
    {
        string path = EditorUtility.OpenFilePanel("Select Music", "", "mp3");
        if (path.Length != 0)
        {
            return path;
        }

        return null;
    }

    IEnumerator GetAudioClip(string path)
    {
        string uri = "file://" + path;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                audioPeer.audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
            }
            loadMusicButton.GetComponentInChildren<TextMeshProUGUI>().text = "Load Music";
            loadMusicButton.GetComponent<Button>().interactable = true;
            playButton.SetActive(true);
            stopButton.SetActive(true);
        }
    }

    public void OnToggleRandom()
    {
        randomLight = !randomLight;
    }

    public void OnUpdateVolume(float volume)
    {
        musicVolume = volume;
    }

    public void OnUpdateMusicTime(float range)
    {
        // range is between 0 and 1
        float musicTime = audioPeer.audioSource.clip.length * range;
        audioPeer.audioSource.time = musicTime;
    }

    public float GetCurrentAudioTime()
    {
        return audioPeer.audioSource.time;
    }

    public float GetTotalAudioTime()
    {
        return audioPeer.audioSource.clip.length;
    }
}
