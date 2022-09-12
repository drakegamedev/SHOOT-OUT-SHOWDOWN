using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    public float DestructorTime;
    
    private Poolable poolable;

    void OnEnable()
    {
        StartCoroutine(Destruct());
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destruct());
    }

    IEnumerator Destruct()
    {
        poolable = GetComponent<Poolable>();

        yield return new WaitForSeconds(DestructorTime);

        poolable.ReturnToPool();
    }
}
