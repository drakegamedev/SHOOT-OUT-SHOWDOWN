using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Health Class Component
public class Health : MonoBehaviour
{
    public float DefaultHealth;
    public float CurrentHealth { get; set; }
    
    void OnEnable()
    {
        CurrentHealth = DefaultHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = DefaultHealth;
    }

    public virtual void TakeDamage(float damage)
    {

    }

    public virtual void OnDeath()
    {

    }
}
