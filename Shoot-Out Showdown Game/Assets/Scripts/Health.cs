using UnityEngine;

// Abstract Health Class Component
public class Health : MonoBehaviour
{
    public float DefaultHealth;                                     // Default HP
    public float CurrentHealth { get; set; }                        // Current HP

    #region Enable/Disable Functions
    void OnEnable()
    {
        CurrentHealth = DefaultHealth;
    }
    #endregion

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = DefaultHealth;
    }
    #endregion

    #region HP System
    public virtual void TakeDamage(float damage)
    {

    }

    public virtual void OnDeath()
    {

    }
    #endregion
}
