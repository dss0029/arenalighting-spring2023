using UnityEngine;
using UnityEngine.UI;


public class SelectorController : MonoBehaviour
{
    public GameObject TopDeck;
    public GameObject UpperDeck;
    public GameObject LowerDeck;
    public Toggle TopToggle;
    public Toggle UpperToggle;
    public Toggle LowerToggle;

    // Start is called before the first frame update
    void Start()
    {
        TopToggle.onValueChanged.AddListener(delegate { ToggleTopDeck(); });
        UpperToggle.onValueChanged.AddListener(delegate { ToggleUpperDeck(); });
        LowerToggle.onValueChanged.AddListener(delegate { ToggleLowerDeck(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ToggleTopDeck()
    {
        TopDeck.SetActive(!TopDeck.activeSelf);
        // Debug.Log("Top");
    }

    void ToggleUpperDeck()
    {
        UpperDeck.SetActive(!UpperDeck.activeSelf);
        // Debug.Log("Upper");
    }

    void ToggleLowerDeck()
    {
        LowerDeck.SetActive(!LowerDeck.activeSelf);
        // Debug.Log("Lower");
    }
}
