public class BulletHealth : Health
{
    private Poolable poolable;                                               // Poolable Class Reference

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        // Initialize Variables
        poolable = GetComponent<Poolable>();
    }
    #endregion

    #region HP System
    // Take Damage
    public override void TakeDamage(float damage)
    {
        // Set Current Health
        CurrentHealth -= damage;

        // Call Death if HP is 0
        if (CurrentHealth <= 0f)
        {
            CurrentHealth = 0f;
            OnDeath();
        }
    }

    // Death Function
    public override void OnDeath()
    {
        poolable.ReturnToPool();
    }
    #endregion
}
