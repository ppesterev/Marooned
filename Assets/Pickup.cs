using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weapon = null; //TODO structure this better
    [SerializeField] private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        weapon = WeaponsFactory.instance.GetRandomWeapon();
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(lifetime - 2f);
        for (int i = 0; i < 50; i++)
        {
            transform.localScale = transform.localScale - Vector3.one * i * Time.fixedDeltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
