using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [Header("Configuration")]
    [SerializeField] private string[] firstSceneIds;                               // First Scene ID


    private string[] currentSceneIds;                                              // Current Scene ID

    #region Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        foreach (string id in firstSceneIds)
            LoadScene(firstSceneIds);
    }
    #endregion

    #region Loading Scene Sequence
    public void LoadScene(string[] sceneId)
    {
        StartCoroutine(LoadSceneSequence(sceneId));
    }

    IEnumerator LoadSceneSequence(string[] sceneId)
    {
        // Unload Current Scene if There are Any
        if (currentSceneIds != null)
        {
            foreach (string id in currentSceneIds)
            {
                yield return SceneManager.UnloadSceneAsync(id);
            }
        }

        // Unload Unused Assets
        Resources.UnloadUnusedAssets();
        yield return null;

        // Garbage Collection
        GC.Collect();
        yield return null;

        // Load Scenes
        foreach (string id in sceneId)
        {
            yield return SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);
        }
        
        // Set Loaded Scene to Current Scene
        currentSceneIds = sceneId;
    }
    #endregion
}
