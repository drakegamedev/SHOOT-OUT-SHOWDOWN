using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerReadyItem : MonoBehaviour
{
    public Color ReadyColor;
    public TextMeshProUGUI PlayerText;
    public TextMeshProUGUI ReadyText;

    // Private Variables
    private Outline outline;
    
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void Ready()
    {
        outline.effectColor = ReadyColor;
        PlayerText.color = ReadyColor;
        ReadyText.color = ReadyColor;
    }
}
