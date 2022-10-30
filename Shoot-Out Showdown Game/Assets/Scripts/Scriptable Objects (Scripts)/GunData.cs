using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun")]
public class GunData : ScriptableObject
{
    [Header("Name")]
    public string Id;

    [Header("Shooting System")]
    public float Damage;

    [Header("Reload System")]
    public int Ammo;
    public float Cooldown;
    public float ReloadTime;
}
