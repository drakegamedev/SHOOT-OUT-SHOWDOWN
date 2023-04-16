using UnityEngine;

// Abstract Health Class Component
public class Health : MonoBehaviour
{
    [SerializeField] protected float defaultHealth;                 // Default HP
    protected float currentHealth;                                  // Current HP

    private void OnEnable()
    {
        currentHealth = defaultHealth;
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = defaultHealth;
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
