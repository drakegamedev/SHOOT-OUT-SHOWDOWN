using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerReadyItem : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Color readyColor;                      // Ready Color Indicator

    [Header("References")]
    [SerializeField] private TextMeshProUGUI playerText;            // Player Text Reference
    [SerializeField] private TextMeshProUGUI readyText;             // Ready Text Reference

    // Private Variables
    private Outline outline;                                        // Outline Component Reference
    
    // Start is called before the first frame update
    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    /// <summary>
    /// Indicate this Player as Ready
    /// </summary>
    public void Ready()
    {
        outline.effectColor = readyColor;
        playerText.color = readyColor;
        readyText.color = readyColor;
    }
}
