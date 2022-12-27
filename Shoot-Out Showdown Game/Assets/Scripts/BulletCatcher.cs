using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCatcher : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            Debug.Log(collider.gameObject.name + " caught!");
            collider.GetComponent<Poolable>().ReturnToPool();
        }
    }
}
