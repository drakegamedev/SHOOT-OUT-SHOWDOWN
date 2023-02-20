using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HP Base Class
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField] private float defaultHealth;                   // Default HP
    public float CurrentHealth { get; set; }                        // Current HP

    void OnEnable()
    {
        CurrentHealth = defaultHealth;
    }

    #region HP System
    public virtual void TakeDamage(float damage)
    {

    }

    public virtual void OnDeath()
    {

    }
    #endregion
}
