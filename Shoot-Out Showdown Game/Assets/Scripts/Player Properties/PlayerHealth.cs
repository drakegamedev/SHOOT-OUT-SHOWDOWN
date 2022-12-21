using UnityEngine;

public class PlayerHealth : Health
{
    public string DeathEffectId;                                        // Death Effect ID

    // Private Variables
    private PlayerSetup playerSetup;                                    // PlayerSetup Class Reference
    private bool isAlive;                                               // Checks if Player is Alive or Dead

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        CurrentHealth = DefaultHealth;
        isAlive = true;
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

            // Play Hurt SFX
            AudioManager.Instance.Play("hurt");
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
        isAlive = true;
        UIController.Instance.UpdateHealth(playerSetup.PlayerNumber, CurrentHealth);
    }
    #endregion
}
