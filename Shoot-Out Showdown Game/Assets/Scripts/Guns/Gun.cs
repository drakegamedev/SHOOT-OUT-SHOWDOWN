using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData GunType;                                                 // Gun Type
    public Transform FirePoint;                                             // Fire Point
    public string BulletId;                                                 // Bullet ID
    public float Force;                                                     // Bullet Force

    private int currentAmmo;                                                // Current Ammo
    private float reloadTime;                                               // Reload Time
    private float currentShootTime;                                         // Current Shoot Time
    private bool isReloading;                                               // Reloading Status Indicator

    #region Enable/Disable Functions
    void OnEnable()
    {
        isReloading = false;
    }
    #endregion

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        // Initialize Variables
        currentAmmo = GunType.Ammo;
        reloadTime = GunType.ReloadTime;
        currentShootTime = GunType.Cooldown;
    }
    #endregion

    #region Update Functions
    void Update()
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
    #endregion

    #region Reload System
    // Start Reload
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
        currentAmmo = GunType.Ammo;

        // Set Status to Default State
        isReloading = false;
    }
    #endregion

    #region Public Functions
    // Gun Cooldown
    public void GunCooldown()
    {
        if (currentShootTime <= 0f)
        {
            currentShootTime = 0;
        }
        else
        {
            currentShootTime -= Time.deltaTime;
        }
    }

    // Gun Fire
    public void Fire()
    {
        if (currentAmmo > 0 && currentShootTime <= 0f)
        {
            // Get Basic Bullet Prefab from Pool
            GameObject BulletPrefab = ObjectPooler.Instance.SpawnFromPool(BulletId, FirePoint.position, FirePoint.rotation);

            // Add force to the bullet
            Rigidbody2D rigidBody = BulletPrefab.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(FirePoint.up * Force, ForceMode2D.Impulse);

            // Set Cooldown
            currentShootTime = GunType.Cooldown;

            // Play Fire SFX
            AudioManager.Instance.Play("shoot-sfx");

            // Decrease Ammo
            currentAmmo--;
        }
    }

    // Reset Ammo
    public void ResetAmmo()
    {
        currentAmmo = GunType.Ammo;
    }
    #endregion
}
