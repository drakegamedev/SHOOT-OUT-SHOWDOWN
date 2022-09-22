using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TrailRenderer BulletTrail;                                           // Trail Renderer Reference
    
    // Private Variables
    private BulletHealth bulletHealth;                                          // BulletHealth Class Reference

    #region Enable/Disable Functions
    void OnDisable()
    {
        // Clear Bullet Trail
        BulletTrail.Clear();
    }
    #endregion

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        // Initialize Variables
        bulletHealth = GetComponent<BulletHealth>();
    }
    #endregion

    #region Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        bulletHealth.TakeDamage(1f);
    }
    #endregion

    #region Trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Damage the Player that was Hit
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1f);
            
            // Call Bullet Death
            bulletHealth.OnDeath();
        }
    }
    #endregion
}
