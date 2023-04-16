using UnityEngine;

// Handles UI Navigation through Panel Management
public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;

    [System.Serializable]
    private struct PanelData
    {
        public string Id;
        public GameObject PanelObject;
    }

    [Header("Properties")]
    [SerializeField] private PanelData[] panels;

    #region Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    /// <summary>
    /// Activate Selected Panel
    /// </summary>
    /// <param name="id"></param>
    public void ActivatePanel(string id)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].PanelObject.SetActive(panels[i].Id == id);
        }
    }
}