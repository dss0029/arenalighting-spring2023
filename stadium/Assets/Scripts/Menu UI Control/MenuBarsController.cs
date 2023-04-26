using UnityEngine;
using UnityEngine.UI;

public class MenuBarsController : MonoBehaviour
{
    public GameObject colorPanel;
    public GameObject musicPanel;
    public GameObject intensityPanel;
    public GameObject selectorPanel;
    public Button colorButton;
    public Button intensityButton;
    public Button musicButton;
    public Button selectorButton;
    public Color activeColor;
    public Color inactiveColor;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        intensityPanel.SetActive(false);
        musicPanel.SetActive(false);
        colorPanel.SetActive(false);
        selectorPanel.SetActive(false);
        activeColor = new Color(0.2509804f, 0.2509804f, 0.2509804f, 1.0f);
        inactiveColor = new Color(0.1921569f, 0.1921569f, 0.1921569f, 1.0f);
        colorButton.onClick.AddListener(OpenColorMenu);
        intensityButton.onClick.AddListener(OpenIntensityMenu);
        musicButton.onClick.AddListener(OpenMusicMenu);
        selectorButton.onClick.AddListener(OpenSelectorMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (colorPanel.activeSelf)
        {
            colorButton.GetComponent<Image>().color = activeColor;
        }
        else {
            colorButton.GetComponent<Image>().color = inactiveColor;
        }

        if (intensityPanel.activeSelf)
        {
            intensityButton.GetComponent<Image>().color = activeColor;
        }
        else {
            intensityButton.GetComponent<Image>().color = inactiveColor;
        }

        if (musicPanel.activeSelf)
        {
            musicButton.GetComponent<Image>().color = activeColor;
        }
        else {
            musicButton.GetComponent<Image>().color = inactiveColor;
        }

        if (selectorPanel.activeSelf)
        {
            selectorButton.GetComponent<Image>().color = activeColor;
        }
        else
        {
            selectorButton.GetComponent<Image>().color = inactiveColor;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    void OpenColorMenu()
    {
        intensityPanel.SetActive(false);
        musicPanel.SetActive(false);
        selectorPanel.SetActive(false);
        colorPanel.SetActive(!colorPanel.activeSelf);
    }

    void OpenIntensityMenu()
    {
        intensityPanel.SetActive(!intensityPanel.activeSelf);
        musicPanel.SetActive(false);
        selectorPanel.SetActive(false);
        colorPanel.SetActive(false);
    }

    void OpenMusicMenu()
    {
        intensityPanel.SetActive(false);
        selectorPanel.SetActive(false);
        musicPanel.SetActive(!musicPanel.activeSelf);
        colorPanel.SetActive(false);
    }

    void OpenSelectorMenu()
    {
        intensityPanel.SetActive(false);
        musicPanel.SetActive(false);
        selectorPanel.SetActive(!selectorPanel.activeSelf);
        colorPanel.SetActive(false);
    }
}
