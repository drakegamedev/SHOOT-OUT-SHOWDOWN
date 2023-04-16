using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunType;                               // Gun Type
    [SerializeField] private Transform firePoint;                           // Fire Point
    [SerializeField] private string bulletId;                               // Bullet ID
    [SerializeField] private float force;                                                     // Bullet Force

    private int currentAmmo;                                                // Current Ammo
    private float reloadTime;                                               // Reload Time
    private float currentShootTime;                                         // Current Shoot Time
    private bool isReloading;                                               // Reloading Status Indicator

    void OnEnable()
    {
        isReloading = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize Variables
        currentAmmo = gunType.Ammo;
        reloadTime = gunType.ReloadTime;
        currentShootTime = gunType.Cooldown;
    }

    private void Update()
    {
        // Don't Execute if Player is Reloading
        if (isReloading)
            return;

        // Reload if Out of Ammo
        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }

        GunCooldown();
    }

    #region Reload System
    /// <summary>
    /// Start Reload
    /// </summary>
    public void Reload()
    {
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        // Set Status to Reloading
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        // Reload Ammo
        currentAmmo = gunType.Ammo;

        // Set Status to Default State
        isReloading = false;
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Gun Cooldown
    /// </summary>
    public void GunCooldown()
    {
        if (currentShootTime <= 0f)
            currentShootTime = 0;
        else
            currentShootTime -= Time.deltaTime;
    }

    /// <summary>
    /// Gun Fire
    /// </summary>
    public void Fire()
    {
        if (currentAmmo > 0 && currentShootTime <= 0f)
        {
            // Get Basic Bullet Prefab from Pool
            GameObject BulletPrefab = ObjectPooler.Instance.SpawnFromPool(bulletId, firePoint.position, firePoint.rotation);

            // Add force to the bullet
            Rigidbody2D rigidBody = BulletPrefab.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(firePoint.up * force, ForceMode2D.Impulse);

            // Set Cooldown
            currentShootTime = gunType.Cooldown;

            // Play Fire SFX
            AudioManager.Instance.Play("shoot-sfx");

            // Decrease Ammo
            currentAmmo--;
        }
    }

    /// <summary>
    /// Reset Ammo
    /// </summary>
    public void ResetAmmo()
    {
        currentAmmo = gunType.Ammo;
    }
    #endregion
}
