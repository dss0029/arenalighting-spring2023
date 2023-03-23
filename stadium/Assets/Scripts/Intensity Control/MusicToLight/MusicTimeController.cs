using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicTimeController : MonoBehaviour
{
    public MusicToLight musicToLight;
    [SerializeField] private Slider slider;

    [SerializeField] private TMP_Text currentTimeText;
    [SerializeField] private TMP_Text remainingTimeText;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((t) =>
        {
            musicToLight.OnUpdateMusicTime(t);
        });
    }

    // Update is called once per frame
    void Update()
    {
        int currentAudioTime = (int) musicToLight.GetCurrentAudioTime();
        currentTimeText.text = formatTime(currentAudioTime);

        int remainingAudioTime = (int) musicToLight.GetTotalAudioTime() - currentAudioTime;
        remainingTimeText.text = "-" + formatTime(remainingAudioTime);
    }

    string formatTime(int time)
    {
        int minutes = time / 60;
        int seconds = time - minutes * 60;

        return minutes.ToString() + ":" + seconds.ToString("00");
    }
}

