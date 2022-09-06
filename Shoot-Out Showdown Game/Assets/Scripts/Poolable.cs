using UnityEngine;

// For Objects That Will Be Put in the Object Pooler
public class Poolable : MonoBehaviour
{
    // Returns Object back to the Object Pooler
    public void ReturnToPool()
    {
        transform.SetParent(ObjectPooler.Instance.transform);
        gameObject.SetActive(false);
    }
}