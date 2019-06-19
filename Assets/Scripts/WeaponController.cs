using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject muzzle;
    [SerializeField] private string weaponId;
    private GameObject projectilePrefab;
    private Weapon equippedWeapon;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        PlayerInputController.Instance.StartFiring += StartFiring;
        PlayerInputController.Instance.StopFiring += StopFiring;

        equippedWeapon = WeaponsFactory.instance.GetWeapon(weaponId);
        projectilePrefab = WeaponsFactory.instance.GetProjectilePrefab(equippedWeapon.ProjectilePrefabId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartFiring()
    {
        animator.SetBool("isFiring", true);
        StartCoroutine("Fire");
    }

    private void StopFiring()
    {
        animator.SetBool("isFiring", false);
        StopCoroutine("Fire");
    }

    private IEnumerator Fire()
    {
        float spreadRadians = equippedWeapon.SpreadAngle * 2 * Mathf.PI / 360f;
        while (true)
        {
            //raycast or launch projectile

            //Vector3[] directions = new Vector3[equippedWeapon.Multishot];
            for (int i = 0; i < equippedWeapon.Multishot; i++)
            {
                Instantiate(projectilePrefab, muzzle.transform.position,
                            Quaternion.LookRotation(this.transform.forward + this.transform.right * Utility.Gaussian(spreadRadians / 3f), this.transform.up));
            }

            if (equippedWeapon.FullAuto)
                yield return new WaitForSeconds(1.0f / equippedWeapon.FireRate);
            else
                yield break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup == null || pickup.weapon == null)
            return;

        equippedWeapon = pickup.weapon;
        projectilePrefab = WeaponsFactory.instance.GetProjectilePrefab(equippedWeapon.ProjectilePrefabId); //TODO clean up this horseshit
        Destroy(pickup.gameObject);
    }
}
