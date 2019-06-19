using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string Id => id;
    public string ProjectilePrefabId => projectilePrefabId;
    public bool FullAuto => fullAuto;
    public float Damage => damage;
    public float FireRate => fireRate;
    public float SpreadAngle => spreadAngle;
    public int Multishot => multishot;

    [SerializeField] private string id;
    [SerializeField] private string projectilePrefabId;
    [SerializeField] private bool fullAuto;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float spreadAngle;
    [SerializeField] private int multishot;
}
