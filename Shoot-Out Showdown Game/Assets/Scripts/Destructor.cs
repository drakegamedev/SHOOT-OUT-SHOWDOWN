using System.Collections;
using UnityEngine;

// Object Destructor
public class Destructor : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float destructorTime;                          // Destructor Timer Count
    
    // Private Variables
    private Poolable poolable;                                              // Poolable Class Reference

    private void OnEnable()
    {
        StartCoroutine(Destruct());
    }

    // Start is called before the first frame update
    void Start()
    {
        poolable = GetComponent<Poolable>();
    }

    /// <summary>
    /// Initiate Destructor Timer
    /// </summary>
    /// <returns></returns>
    private IEnumerator Destruct()
    {
        yield return new WaitForSeconds(destructorTime);
        poolable.ReturnToPool();
    }
}
