using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class WeaponsFactory : MonoBehaviour
{
    public static WeaponsFactory instance;

    [SerializeField]
    private GameObject[] projectilePrefabs;
    private Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
    private string[] weaponIds;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        string weaponsJson = Resources.Load("Configs/weapons").ToString();
        JSONNode weaponsNode = JSON.Parse(weaponsJson);
        foreach (string key in weaponsNode.Keys)
        {
            string weaponDesc = weaponsNode[key].ToString();
            Weapon weapon = JsonUtility.FromJson<Weapon>(weaponDesc);
            weapons.Add(key, weapon);
        }

        weaponIds = new string[weapons.Count];
        weapons.Keys.CopyTo(weaponIds, 0);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public Weapon GetWeapon(string id)
    {
        return weapons[id];   
    }

    //Need to organise this better
    public Weapon GetRandomWeapon()
    {
        return weapons[weaponIds[Random.Range(0, weaponIds.Length)]];
    }

    //TODO: change to use dictionary?
    public GameObject GetProjectilePrefab(string id)
    {
        foreach (GameObject prefab in projectilePrefabs)
        {
            if (prefab.name == id)
                return prefab;
        }
        return null;
    }
}
