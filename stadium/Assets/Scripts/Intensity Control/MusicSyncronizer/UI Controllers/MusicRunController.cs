using UnityEngine;
using UnityEngine.UI;

public class MusicRunController : MonoBehaviour
{

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject stopButton;

    public void OnMusicStateChanged(MusicState musicState)
    {
        if (musicState == MusicState.Play)
        {
            playButton.SetActive(false);
            pauseButton.SetActive(true);
        }
        else if (musicState == MusicState.Pause)
        {
            playButton.SetActive(true);
            pauseButton.SetActive(false);
        }
        else if (musicState == MusicState.Stop)
        {
            playButton.SetActive(true);
            pauseButton.SetActive(false);
        }
        else if (musicState == MusicState.NewMusicLoaded)
        {
            playButton.SetActive(true);
            pauseButton.SetActive(false);
        }
    }
}
