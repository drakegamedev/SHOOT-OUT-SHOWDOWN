public class BulletHealth : Health
{
    private Poolable poolable;                                               // Poolable Class Reference

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize Variables
        poolable = GetComponent<Poolable>();
    }

    #region HP System
    /// <summary>
    /// Take Damage
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(float damage)
    {
        // Set Current Health
        currentHealth -= damage;

        // Call Death if HP is 0
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            OnDeath();
        }
    }

    /// <summary>
    /// Death Function
    /// </summary>
    public override void OnDeath()
    {
        poolable.ReturnToPool();
    }
    #endregion
}
