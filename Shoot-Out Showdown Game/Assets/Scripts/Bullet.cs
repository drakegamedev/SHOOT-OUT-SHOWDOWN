using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TrailRenderer BulletTrail;
    
    private Rigidbody2D rb;
    private BulletHealth bulletHealth;

    void OnDisable()
    {
        BulletTrail.Clear();    
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletHealth = GetComponent<BulletHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bulletHealth.TakeDamage(1f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1f);
            bulletHealth.OnDeath();
        }

        
    }
}
