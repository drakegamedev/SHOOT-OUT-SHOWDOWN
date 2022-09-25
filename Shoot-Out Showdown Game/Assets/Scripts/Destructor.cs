using System.Collections;
using UnityEngine;

// Object Destructor
public class Destructor : MonoBehaviour
{
    public float DestructorTime;                                            // Destructor Timer Count
    
    // Private Variables
    private Poolable poolable;                                              // Poolable Class Reference

    #region Enable/Disable Functions
    void OnEnable()
    {
        StartCoroutine(Destruct());
    }
    #endregion

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destruct());
    }
    #endregion

    #region Coroutines
    // Initiate Destructor Timer
    IEnumerator Destruct()
    {
        poolable = GetComponent<Poolable>();

        yield return new WaitForSeconds(DestructorTime);

        poolable.ReturnToPool();
    }
    #endregion
}
