using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public GameObject PlayerGraphic;
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

        if (CurrentHealth <= 0f)
        {
            CurrentHealth = 0f;
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        GameManager.Instance.PlayerList.Remove(gameObject);
        GameManager.Instance.SetGame();
        
        StartCoroutine(DeathEffect());
    }

    IEnumerator DeathEffect()
    {
        PlayerGraphic.SetActive(false);
        playerSetup.PlayerInput.enabled = false;
        Poolable deathEffect = ObjectPooler.Instance.SpawnFromPool(DeathEffectId, transform.position, Quaternion.identity).GetComponent<Poolable>();

        yield return new WaitForSeconds(1f);

        deathEffect.ReturnToPool();
    }
}
