using UnityEngine;

public class BulletCatcher : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
            collider.GetComponent<Poolable>().ReturnToPool();
    }
}
