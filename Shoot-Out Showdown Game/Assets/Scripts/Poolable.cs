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
}