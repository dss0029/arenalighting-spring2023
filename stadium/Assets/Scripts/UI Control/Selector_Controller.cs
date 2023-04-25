using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Selector_Controller : MonoBehaviour
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
        TopToggle.onValueChanged.AddListener(delegate { toggleTopDeck(); });
        UpperToggle.onValueChanged.AddListener(delegate { toggleUpperDeck(); });
        LowerToggle.onValueChanged.AddListener(delegate { toggleLowerDeck(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void toggleTopDeck()
    {
        TopDeck.SetActive(!TopDeck.activeSelf);
        Debug.Log("Top");
    }

    void toggleUpperDeck()
    {
        UpperDeck.SetActive(!UpperDeck.activeSelf);
        Debug.Log("Upper");
    }

    void toggleLowerDeck()
    {
        LowerDeck.SetActive(!LowerDeck.activeSelf);
        Debug.Log("Lower");
    }
}
