using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject loadingPanel;                                   // Loading Panel GameObject Reference

    /// <summary>
    /// Activates/Deactivates Loading Panel Whenever Going From
    /// One Scene to Another
    /// </summary>
    /// <param name="value"></param>
    public void SetLoading(bool value)
    {
        loadingPanel.SetActive(value);
    }
}
