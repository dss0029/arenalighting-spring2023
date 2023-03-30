using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{

    public MusicSynchronizer musicToLight;
    [SerializeField] private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            musicToLight.UpdateVolume(v);
        });
    }
}
