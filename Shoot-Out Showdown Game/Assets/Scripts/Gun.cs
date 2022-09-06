using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData GunType;

    private int currentAmmo;
    private float reloadTime;
    private float currentShootTime;
    private bool isReloading;

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
}
