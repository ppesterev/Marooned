using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // First test: spawnDelay(time) = baseTime/(1 + time^2)
    // adjusted so that spawn rate doubles after 1 minute (not anymore)

    //TODO: cap enemies/sth else to prevent too many spawning?

    //TODO: object pool

    [SerializeField] private float spawnDistance = 10f;
    [SerializeField] private GameObject enemyPrefab;
    private GameObject player;

    [SerializeField] private readonly float timeFactor = 60f;
    [SerializeField] private readonly float baseDelay = 2f;

    private float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnDelay = baseDelay;
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnLoop()
    {
        while(true)
        {
            Debug.Log("Spawning at time " + Time.time);
            Spawn();
            float adjustedTime = Time.time / timeFactor;
            spawnDelay = baseDelay / (1 + adjustedTime * adjustedTime);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Spawn()
    {
        Vector3 playerPos = player.transform.position;
        // create a vector going spawnDistance in a random direction
        Vector3 offset = Quaternion.Euler(0, Random.Range(0, 360f), 0) * Vector3.forward * spawnDistance;
        GameObject newEnemy = Instantiate(enemyPrefab, playerPos + offset, Quaternion.identity, transform);
        newEnemy.transform.localScale = Vector3.one * Random.Range(0.9f, 1.2f);
    }
}
