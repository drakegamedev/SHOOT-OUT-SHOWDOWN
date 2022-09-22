using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public string DeathEffectId;

    private PlayerSetup playerSetup;


    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        CurrentHealth = DefaultHealth;
    }

    public override void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        UIController.Instance.UpdateHealth(playerSetup.PlayerNumber, CurrentHealth);

        if (CurrentHealth > 0f)
        {
            playerSetup.Animator.SetTrigger("hurt");
        }
        else if (CurrentHealth <= 0f)
        {
            CurrentHealth = 0f;
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        GameManager.Instance.SetGame();
        DeathEffect();
    }

    void DeathEffect()
    {
        gameObject.SetActive(false);
        ObjectPooler.Instance.SpawnFromPool(DeathEffectId, transform.position, Quaternion.identity);
    }

    public void ResetHealth()
    {
        CurrentHealth = DefaultHealth;
        UIController.Instance.UpdateHealth(playerSetup.PlayerNumber, CurrentHealth);
    }
}
