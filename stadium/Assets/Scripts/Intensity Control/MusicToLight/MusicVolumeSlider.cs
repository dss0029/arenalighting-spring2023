using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{

    public MusicToLight musicToLight;
    [SerializeField] private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            musicToLight.OnUpdateVolume(v);
        });
    }
}
