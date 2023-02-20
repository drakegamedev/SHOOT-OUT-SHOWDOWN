using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerReadyItem : MonoBehaviour
{
    [SerializeField] private Color readyColor;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI readyText;

    // Private Variables
    private Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void Ready()
    {
        outline.effectColor = readyColor;
        playerText.color = readyColor;
        readyText.color = readyColor;
    }
}
