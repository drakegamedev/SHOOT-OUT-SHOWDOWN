using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private string deathEffectId;                      // Death Effect ID

    // Private Variables
    private PlayerSetup playerSetup;                                    // PlayerSetup Class Reference
    private bool isAlive;                                               // Checks if Player is Alive or Dead

    // Start is called before the first frame update
    private void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        currentHealth = defaultHealth;
        isAlive = true;
    }

    #region HP System
    public override void TakeDamage(float damage)
    {
        if (!isAlive)
            return;

        // Decrease HP
        currentHealth -= damage;

        // Update HP on UI
        UIController.Instance.UpdateHealth(playerSetup.PlayerNumber, currentHealth);

        if (currentHealth > 0f)
        {
            // Visual Indicator of Player Taking Damage
            playerSetup.Animator.SetTrigger("hurt");

            // Play Hurt SFX
            AudioManager.Instance.Play("hurt");
        }
        else if (currentHealth <= 0f)
        {
            // Clamp HP to 0
            currentHealth = 0f;
            
            // Kill This Player
            OnDeath();
        }
    }

    // Player Death
    public override void OnDeath()
    {
        if (!isAlive)
            return;

        // Declare Round Over
        GameManager.Instance.RoundOver();

        // Play Explosion SFX
        AudioManager.Instance.Play("explosion");

        // Activate Death VFX
        DeathEffect();

        isAlive = false;
    }
    #endregion

    #region Death VFX
    /// <summary>
    /// Creates a Visual Indication that the Player has Died
    /// </summary>
    private void DeathEffect()
    {
        gameObject.SetActive(false);
        ObjectPooler.Instance.SpawnFromPool(deathEffectId, transform.position, Quaternion.identity);
    }
    #endregion

    /// <summary>
    /// Resets HP Properties
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = defaultHealth;
        isAlive = true;
        UIController.Instance.UpdateHealth(playerSetup.PlayerNumber, currentHealth);
    }
}
