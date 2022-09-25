using UnityEngine;

// For Objects That Will Be Put in the Object Pooler
public class Poolable : MonoBehaviour
{
    #region Enable/Disable Functions
    void OnEnable()
    {
        EventManager.Instance.PlayerDied += GameCleanUp;
    }

    void OnDisable()
    {
        EventManager.Instance.PlayerDied += GameCleanUp;
    }
    #endregion

    #region Public Functions
    // Returns Object back to the Object Pooler
    public void ReturnToPool()
    {
        transform.SetParent(ObjectPooler.Instance.transform);
        gameObject.SetActive(false);
    }

    public void GameCleanUp()
    {
        ReturnToPool();
    }
    #endregion
}