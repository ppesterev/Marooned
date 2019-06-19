using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance { get; private set; }

    public Action StartFiring;
    public Action StopFiring;
    public Action Cast;

    private Camera mainCamera;
    private Animator animator;
    public GameObject aimingLine { get; private set; }

    private int aimingPlaneMask = (1 << 8);
    public bool stationary { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();

        aimingLine = GameObject.Find("AimingLine");
        GetComponent<Unit>().UnitDeath += () => { enabled = false; };
    }

    // Update is called once per frame
    void Update()
    {
        if (!stationary)
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, aimingPlaneMask))
            {
                Vector3 direction = hit.point - transform.position;
                direction.y = 0;

                // adjust for aiming line parallax
                float longSide = direction.magnitude;
                float shortSide = aimingLine.transform.localPosition.x;
                float distance = Mathf.Sqrt(longSide * longSide + shortSide * shortSide);
                float angle = Mathf.Asin(shortSide / distance) * 180f / Mathf.PI;

                direction = Quaternion.Euler(0, -angle, 0) * direction;

                transform.forward = direction;
            }

            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            Vector3 velocity = new Vector3(horiz, 0, vert);
            velocity *= (velocity.magnitude < 1f ? 1f : 1f / velocity.magnitude); // clamp velocity to 1
            transform.position += velocity * 5f * Time.deltaTime;

            float ViewToVelocityRadians = Vector3.SignedAngle(transform.forward, velocity, Vector3.up) * Mathf.PI / 180f;
            animator.SetFloat("ViewRelativeVelocityX", Mathf.Sin(ViewToVelocityRadians) * velocity.magnitude);
            animator.SetFloat("ViewRelativeVelocityZ", Mathf.Cos(ViewToVelocityRadians) * velocity.magnitude);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StartFiring();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopFiring();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Cast();
        }
    }

    //private void SetupAiming()
    //{
    //    GameObject muzzle = GameObject.Find("Muzzle"); // TODO checks and balances
    //    aimingLine = new GameObject();
    //    aimingLine.transform.SetParent(this.transform);

    //    Debug.Log($"muzzle: {muzzle.transform.position}, player: {transform.position}");

    //    Vector3 aimingLinePosition = muzzle.transform.position - transform.position;
    //    aimingLinePosition.z = 0;

    //    Debug.Log($"aim: {aimingLinePosition}");

    //    aimingLine.transform.localPosition = aimingLinePosition;
    //    aimingLine.transform.rotation = transform.rotation;
    //}
}
