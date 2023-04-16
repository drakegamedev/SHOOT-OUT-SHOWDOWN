using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer bulletTrail;                          // Trail Renderer Reference
    
    // Private Variables
    private BulletHealth bulletHealth;                                          // BulletHealth Class Reference

    private void OnDisable()
    {
        // Clear Bullet Trail
        bulletTrail.Clear();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize Variables
        bulletHealth = GetComponent<BulletHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bulletHealth.TakeDamage(1f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
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
}
