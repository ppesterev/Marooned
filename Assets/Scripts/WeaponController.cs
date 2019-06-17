using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject projectilePrefab;
    private Weapon equippedWeapon;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        equippedWeapon = new Weapon("laser rifle", projectilePrefab, true, 20, 3, 1, 1);
        PlayerInputController.Instance.StartFiring += StartFiring;
        PlayerInputController.Instance.StopFiring += StopFiring;
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
        float spread = equippedWeapon.SpreadAngle * 2 * Mathf.PI / 360f;
        while (true)
        {
            //raycast or launch projectile
            //Debug.DrawRay(muzzle.transform.position, (this.transform.forward + this.transform.right * GetGaussian(spread / 3f)) * 100, Color.cyan, 0.05f);

            Vector3[] directions = new Vector3[equippedWeapon.Multishot];
            for (int i = 0; i < directions.Length; i++)
            {
                Instantiate(equippedWeapon.ProjectilePrefab, muzzle.transform.position,
                            Quaternion.LookRotation(this.transform.forward + this.transform.right * Utility.Gaussian(spread / 3f), this.transform.up));
            }

            if (equippedWeapon.FullAuto)
                yield return new WaitForSeconds(1.0f / equippedWeapon.FireRate);
            else
                yield break;
        }
    }
}
