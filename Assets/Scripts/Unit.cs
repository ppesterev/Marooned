using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Animator animator;

    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    private bool dead = false;

    public System.Action UnitDeath;
    public System.Action<int, int> HealthChanged;

    // Start is called before the first frame update
    void Awake()
    {
        Init(100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int health)
    {
        Health = health;
        MaxHealth = health;
    }

    public void TakeDamage(int value)
    {
        if (dead)
            return;

        Health -= value;
        HealthChanged?.Invoke(Health, MaxHealth);

        if (Health <= 0)
        {
            dead = true;
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        UnitDeath?.Invoke();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
