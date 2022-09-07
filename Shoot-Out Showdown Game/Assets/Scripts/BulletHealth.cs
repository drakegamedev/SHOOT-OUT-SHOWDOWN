using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHealth : Health
{
    private Poolable poolable;
    
    void Start()
    {
        poolable = GetComponent<Poolable>();
    }

    public override void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0f)
        {
            CurrentHealth = 0f;
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        poolable.ReturnToPool();
    }
}
