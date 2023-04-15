using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    
    [Header("Configuration")]
    public string FirstSceneId;                                                 // First Scene ID
    [SerializeField] private LoadingScreen loadingScreen;

    // Private Variables
    private string currentSceneId;                                              // Current Scene ID

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
        LoadScene(FirstSceneId);
    }
    #endregion

    #region Loading Scene Sequence
    public void LoadScene(string sceneId)
    {
        StartCoroutine(LoadSceneSequence(sceneId));
    }

    IEnumerator LoadSceneSequence(string sceneId)
    {
        // Activate Loading Screen
        loadingScreen.SetLoading(true);

        // Unload Current Scene if There are Any
        if (currentSceneId != null)
            yield return SceneManager.UnloadSceneAsync(currentSceneId);
        
        // Unload Unused Assets
        Resources.UnloadUnusedAssets();
        yield return null;

        // Garbage Collection
        GC.Collect();
        yield return null;

        // Load the Scene
        yield return SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);

        // Activate Loading Screen
        loadingScreen.SetLoading(false);

        // Set Loaded Scene to Current Scene
        currentSceneId = sceneId;
    }
    #endregion
}
