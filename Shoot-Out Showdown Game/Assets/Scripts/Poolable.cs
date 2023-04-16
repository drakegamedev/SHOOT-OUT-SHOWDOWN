using UnityEngine;

// For Objects That Will Be Put in the Object Pooler
public class Poolable : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.PlayerDied += GameCleanUp;
    }

    private void OnDisable()
    {
        EventManager.Instance.PlayerDied += GameCleanUp;
    }

    /// <summary>
    /// Returns Object back to the Object Pooler
    /// </summary>
    public void ReturnToPool()
    {
        transform.SetParent(ObjectPooler.Instance.transform);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Cleans Up All Unnecessary Objects
    /// </summary>
    public void GameCleanUp()
    {
        ReturnToPool();
    }
}