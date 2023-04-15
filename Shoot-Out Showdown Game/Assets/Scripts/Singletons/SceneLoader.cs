using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    
    [Header("Configurations")]
    [SerializeField] private string firstSceneId;                                                 // First Scene ID
    [SerializeField] private LoadingScreen loadingScreen;

    // Private Variables
    private string[] currentSceneIds;                                                             // Current Scene IDs

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

    // Start is called before the first frame update
    private void Start()
    {
        string[] scenes = { firstSceneId };

        LoadScene(scenes);
    }

    /// <summary>
    /// Call Load Scene
    /// </summary>
    /// <param name="sceneIds"></param>
    public void LoadScene(string[] sceneIds)
    {
        StartCoroutine(LoadSceneSequence(sceneIds));
    }

    /// <summary>
    /// Initiate Loading Sequence
    /// </summary>
    /// <param name="sceneIds"></param>
    /// <returns></returns>
    IEnumerator LoadSceneSequence(string[] sceneIds)
    {
        // Activate Loading Screen
        loadingScreen.SetLoading(true);

        // Unload Current Scenes if There are Any
        if (currentSceneIds != null)
        {
            foreach (string id in currentSceneIds)
                yield return SceneManager.UnloadSceneAsync(id);
        }

        // Unload Unused Assets
        Resources.UnloadUnusedAssets();
        yield return null;

        // Garbage Collection
        GC.Collect();
        yield return null;

        // Load the Scenes
        foreach (string id in sceneIds)
            yield return SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);

        // Activate Loading Screen
        loadingScreen.SetLoading(false);

        // Set Loaded Scene to Current Scene
        currentSceneIds = sceneIds;
    }
}
