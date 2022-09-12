using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    
    [Header("Configuration")]
    public string FirstSceneId;

    private string currentSceneId;

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

    private void Start()
    {
        LoadScene(FirstSceneId);
    }

    private IEnumerator LoadSceneSequence(string sceneId)
    {
        /*if (currentSceneId != null)
        {
            /*AdditionalSceneLoader additionalScene = AdditionalSceneLoader.Instance;

            if (additionalScene)
            {
                yield return additionalScene.UnloadScenes();
            }

            yield return SceneManager.UnloadSceneAsync(currentSceneId);
            currentSceneId = null;
        }*/

        if (currentSceneId != null)
        {
            yield return SceneManager.UnloadSceneAsync(currentSceneId);
        }

        Resources.UnloadUnusedAssets();
        yield return null;
        GC.Collect();
        yield return null;

        yield return SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);

        currentSceneId = sceneId;
    }

    public Coroutine LoadScene(string sceneId)
    {
        return StartCoroutine(LoadSceneSequence(sceneId));
    }
}
