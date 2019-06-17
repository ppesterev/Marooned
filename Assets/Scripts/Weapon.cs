using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string Name { get; private set; }
    public GameObject ProjectilePrefab { get; private set; }
    public bool FullAuto { get; private set; }
    public float Damage { get; private set; }
    public float FireRate { get; private set; }
    public float SpreadAngle { get; private set; }
    public int Multishot { get; private set; }

    public Weapon(string name, GameObject projectilePrefab, bool fullauto, float damage, float firerate, float spreadangle, int multishot)
    {
        Name = name;
        ProjectilePrefab = projectilePrefab;
        FullAuto = fullauto;
        Damage = damage;
        FireRate = firerate;
        SpreadAngle = spreadangle;
        Multishot = multishot;
    }
}
