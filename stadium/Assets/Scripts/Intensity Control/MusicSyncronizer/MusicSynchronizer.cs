using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum MusicState
{
    Play,
    Pause,
    Stop,
    NewMusicLoaded
}

public enum MusicSynchronizerMode
{
    Linear,
    Random,
    Amplitude
}

public class MusicSynchronizer : MonoBehaviour
{
    public AudioPeer audioPeer;

    public float minScale = 0.25f;
    public float maxScale = 0.35f;
    public bool useBuffer = true;


    [SerializeField] private UnityEvent<MusicState> musicStateChangeEvent;

    public Toggle synchronizeToggle;
    public GameObject loadMusicButton;
    public GameObject musicToLightModeText;
    [SerializeField] private MusicSynchronizerMode currentSynchronizerMode = MusicSynchronizerMode.Linear;

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
        if (!synchronizeToggle.isOn) return;

        if (currentSynchronizerMode == MusicSynchronizerMode.Linear) {
            linearMode();
        }
        else if (currentSynchronizerMode == MusicSynchronizerMode.Random)
        {
            randomMode();
        }
        else if (currentSynchronizerMode == MusicSynchronizerMode.Amplitude)
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
        musicStateChangeEvent.Invoke(MusicState.Play);

        if (audioPeer.audioSource.time > 0)
        {
            audioPeer.audioSource.UnPause();
            return;
        }

        audioPeer.audioSource.Play();
    }

    public void OnPauseMusic()
    {
        musicStateChangeEvent.Invoke(MusicState.Pause);

        audioPeer.audioSource.Pause();
    }

    public void OnStopMusic()
    {
        musicStateChangeEvent.Invoke(MusicState.Stop);

        StopMusic();
    }

    void StopMusic()
    {
        audioPeer.audioSource.Stop();
        audioPeer.ResetValues();
    }

    public void LoadMusic()
    {
        string path = OpenFilePanel();
        if (path != null) {
            StopMusic();
            musicStateChangeEvent.Invoke(MusicState.NewMusicLoaded);
            
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
                UpdateMusic(DownloadHandlerAudioClip.GetContent(www));
            }

            loadMusicButton.GetComponentInChildren<TextMeshProUGUI>().text = "Load Music";
            loadMusicButton.GetComponent<Button>().interactable = true;
        }
    }

    void UpdateMusic(AudioClip clip)
    {
        audioPeer.audioSource.clip = clip;
        musicStateChangeEvent.Invoke(MusicState.NewMusicLoaded);
    }

    public void ToggleRandom()
    {
        randomLight = !randomLight;
    }

    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
    }

    public void UpdateMusicTime(float range)
    {
        // range is between 0 and 1
        float musicTime = audioPeer.audioSource.clip.length * range;
        audioPeer.audioSource.time = musicTime;
    }

    public float CurrentAudioTime()
    {
        return audioPeer.audioSource.time;
    }

    public float TotalAudioTime()
    {
        return audioPeer.audioSource.clip.length;
    }

    public void UpdateSynchronizerMode(MusicSynchronizerMode mode)
    {
        currentSynchronizerMode = mode;
    }
}
