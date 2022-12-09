using UnityEngine;

public class PlayerHealth : Health
{
    public string DeathEffectId;                                        // Death Effect ID

    // Private Variables
    private PlayerSetup playerSetup;                                    // PlayerSetup Class Reference

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        CurrentHealth = DefaultHealth;
    }
    #endregion

    #region HP System
    public override void TakeDamage(float damage)
    {
        // Decrease HP
        CurrentHealth -= damage;

        // Update HP on UI
        UIController.Instance.UpdateHealth(playerSetup.PlayerNumber, CurrentHealth);

        if (CurrentHealth > 0f)
        {
            // Visual Indicator of Player Taking Damage
            playerSetup.Animator.SetTrigger("hurt");
        }
        else if (CurrentHealth <= 0f)
        {
            // Clamp HP to 0
            CurrentHealth = 0f;
            
            // Kill This Player
            OnDeath();
        }
    }

    // Player Death
    public override void OnDeath()
    {
        // Declare Round Over
        GameManager.Instance.RoundOver();

        // Play Explosion SFX
        AudioManager.Instance.Play("explosion");

        // Activate Death VFX
        DeathEffect();
    }
    #endregion

    #region Death VFX
    void DeathEffect()
    {
        gameObject.SetActive(false);
        ObjectPooler.Instance.SpawnFromPool(DeathEffectId, transform.position, Quaternion.identity);
    }
    #endregion

    #region Public Functions
    public void ResetHealth()
    {
        CurrentHealth = DefaultHealth;
        UIController.Instance.UpdateHealth(playerSetup.PlayerNumber, CurrentHealth);
    }
    #endregion
}
