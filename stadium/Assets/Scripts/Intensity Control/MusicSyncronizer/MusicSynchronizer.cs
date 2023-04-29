using SimpleFileBrowser;
using System.Collections;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
    string audioFilePath = string.Empty;

    void Start()
    {
        audioPeer.audioSource.playOnAwake = false;
        audioPeer.audioSource.Stop();
        allLEDs = GameObject.FindGameObjectsWithTag("LED");

        FileBrowser.SetFilters(true, new FileBrowser.Filter("Audio Files", ".mp3"));
        FileBrowser.SetDefaultFilter(".mp3");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
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
        #if UNITY_EDITOR
        EditorLoadMusic();
        #else
        StartCoroutine(ApplicationLoadMusicCoroutines());
        #endif
    }

    string OpenFilePanel()
    {
        #if UNITY_EDITOR
        string path = EditorUtility.OpenFilePanel("Select Music", "", "mp3");
        if (path.Length != 0)
        {
            return path;
        }
        #endif

        return null;
    }

    void EditorLoadMusic()
    {
        // Unity Editor Version
        string path = OpenFilePanel();
        if (path != null)
        {
            StopMusic();
            musicStateChangeEvent.Invoke(MusicState.NewMusicLoaded);

            loadMusicButton.GetComponentInChildren<TextMeshProUGUI>().text = "Loading...";
            loadMusicButton.GetComponent<Button>().interactable = false;
            StartCoroutine(GetAudioClip(path));
        }
    }

    IEnumerator ApplicationLoadMusicCoroutines()
    {
        yield return ShowFilePanelCourotine();

        if (audioFilePath.Length != 0)
        {
            StopMusic();
            musicStateChangeEvent.Invoke(MusicState.NewMusicLoaded);

            loadMusicButton.GetComponentInChildren<TextMeshProUGUI>().text = "Loading...";
            loadMusicButton.GetComponent<Button>().interactable = false;
            StartCoroutine(GetAudioClip(audioFilePath));
        }
    }

    IEnumerator ShowFilePanelCourotine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load MP3 Audio File", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        // Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            audioFilePath = FileBrowser.Result[0];
            audioFilePath = audioFilePath.Replace("\\", "/");
        }
        else
        {
            audioFilePath = string.Empty;
        }
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
        audioPeer.audioSource.volume = volume;
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
