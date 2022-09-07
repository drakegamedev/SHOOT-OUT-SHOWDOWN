using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditionalSceneLoader : MonoBehaviour
{
    public static AdditionalSceneLoader Instance;
    
    public List<string> AdditionScenes = new();

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
        StartCoroutine(LoadSceneSequence());
    }

    public IEnumerator LoadSceneSequence()
    {
        for (int i = 0; i < AdditionScenes.Count; i++)
        {
            yield return SceneManager.LoadSceneAsync(AdditionScenes[i], LoadSceneMode.Additive);
        }
    }

    private IEnumerator UnloadSceneSequence()
    {
        for (int i = 0; i < AdditionScenes.Count; i++)
        {
            yield return SceneManager.UnloadSceneAsync(AdditionScenes[i]);
        }
    }

    public Coroutine UnloadScenes()
    {
        return StartCoroutine(UnloadSceneSequence());
    }
}
