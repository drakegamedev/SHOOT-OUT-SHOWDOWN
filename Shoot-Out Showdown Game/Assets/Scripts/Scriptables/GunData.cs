using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun")]
public class GunData : ScriptableObject
{
    [Header("Name")]
    public string Id;                                       // Gun Name

    [Header("Shooting System")]
    public float Damage;                                    // Gun Damage

    [Header("Reload System")]
    public int Ammo;                                        // Ammo Amount
    public float Cooldown;                                  // Gun Cooldown
    public float ReloadTime;                                // Gun Reload Timer
}
