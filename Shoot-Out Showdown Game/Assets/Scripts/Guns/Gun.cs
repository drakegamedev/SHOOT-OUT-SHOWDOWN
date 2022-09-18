using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData GunType;
    public Transform FirePoint;
    public string BulletId;
    public float Force;

    private int currentAmmo;
    private float reloadTime;
    private float currentShootTime;
    private bool isReloading;

    void OnEnable()
    {
        isReloading = false;
        //EventManager.Instance.ResetMatch += ResetAmmo;
    }

    void OnDisable()
    {
        //EventManager.Instance.ResetMatch -= ResetAmmo;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = GunType.Ammo;
        reloadTime = GunType.ReloadTime;
        currentShootTime = GunType.Cooldown;
    }

    void Update()
    {        
        if (isReloading)
            return;
        
        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }

        GunCooldown();
    }

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

    public void Fire()
    {
        if (currentAmmo > 0 && currentShootTime <= 0f)
        {
            // Get Basic Bullet Prefab from Pool
            GameObject BulletPrefab = ObjectPooler.Instance.SpawnFromPool(BulletId, FirePoint.position, FirePoint.rotation);

            // Add force to the bullet
            Rigidbody2D rigidBody = BulletPrefab.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(FirePoint.up * Force, ForceMode2D.Impulse);

            currentShootTime = GunType.Cooldown;
            currentAmmo--;
        }
    }

    public void Reload()
    {
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        
        yield return new WaitForSeconds(reloadTime);
        
        currentAmmo = GunType.Ammo;
        isReloading = false;
    }

    public void ResetAmmo()
    {
        currentAmmo = GunType.Ammo;
    }
}
