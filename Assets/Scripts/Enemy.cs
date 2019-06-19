using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator = null;

    private Unit player = null;
    private Vector3 direction = default;

    private float speed = 0.8f;
    private float turningSpeed = 0.8f;
    private float attackSpeed = 0.2f;
    private float attackDelay = 0.1f;

    private float attackTimeout = 0f;

    [SerializeField] private GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        animator = GetComponentInChildren<Animator>();
        GetComponent<Unit>().UnitDeath += Die;
        //GetComponent<Unit>().HealthChanged += (int health, int maxHealth) => { }; // this could be the hit reaction
        StartCoroutine(SetDirection());
    }

    public void Init(float speed, float turningSpeed, float attackTime, float attackSpeed)
    {
        this.speed = speed;
        this.turningSpeed = turningSpeed;
        this.attackDelay = attackTime;
        this.attackSpeed = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
    
        transform.forward = Vector3.RotateTowards(transform.forward, direction.normalized, turningSpeed * Time.deltaTime, 0);

        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
            if (attackTimeout > attackSpeed)
            {
                StartCoroutine("Attack");
                attackTimeout = 0f;
            }
            else
                attackTimeout += Time.deltaTime;
        else
            transform.position += this.transform.forward * speed * Time.deltaTime;
    }

    // once in a while, set direction to face the player with some random deviation
    private IEnumerator SetDirection()
    {
        while(player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0;

            Vector3 offset = Quaternion.Euler(0, 90, 0) * directionToPlayer;
            direction = directionToPlayer + offset.normalized * Utility.Gaussian(directionToPlayer.magnitude / 3f);
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
            player.TakeDamage(10); 
    }

    //TODO: repeated hits shouldn't replay animation
    private void Die()
    {
        StopAllCoroutines();
        if (Random.Range(0f, 1f) < 0.3f)
            Instantiate(drop, transform.position, Quaternion.identity);
        transform.forward = Quaternion.Euler(0, Utility.Gaussian(45), 0) * transform.forward;
        animator.SetTrigger("Die");
        this.enabled = false;
    }
}
