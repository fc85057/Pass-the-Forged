using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject bearTrapPrefab;
    [SerializeField]
    private GameObject potionPrefab;
    [SerializeField]
    private GameObject firePrefab;
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private EnemyStats[] enemyTypes;

    //[SerializeField]
    //private GameObject[] obstacles;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float checkRadius = 2f;

    [SerializeField]
    private float minSpawnTime = 3f;

    [SerializeField]
    private float maxSpawnTime = 8f;

    private Vector2 vikingPosition;
    private float nextSpawnTime;
    private const float xDifferential = 13.5f;

    private void Update()
    {
        vikingPosition = GameManager.Instance.CurrentViking.transform.position;
        Vector2 spawnPoint = new Vector2(vikingPosition.x + 13.5f, 0);

        if (Time.time > nextSpawnTime && !Physics2D.OverlapCircle(spawnPoint, checkRadius, layerMask))
        {
            Spawn();
        }
        
    }

    private void Spawn()
    {
        int chance = Random.Range(0, 100);
        if (chance < 50)
        {
            SpawnEnemy();
        }
        else
        {
            SpawnObstacle();
        }

        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);

    }

    private void SpawnEnemy()
    {
        EnemyStats newEnemyStats = enemyTypes[Random.Range(0, enemyTypes.Length)];

        GameObject newEnemy = Instantiate(enemyPrefab, new Vector2(vikingPosition.x + xDifferential, -2.81f), Quaternion.identity);
        newEnemy.GetComponent<Enemy>().SetStats(newEnemyStats);
    }

    private void SpawnObstacle()
    {
        // GameObject obstacleToSpawn = obstacles[Random.Range(0, obstacles.Length)];
        // ugly how y differentials are hard-coded here - should be handled by respective scripts
        int chance = Random.Range(0, 100);

        if (chance > 75)
        {
            Instantiate(bearTrapPrefab, new Vector2(vikingPosition.x + xDifferential, -4.75f), Quaternion.identity);
        }
        else if (chance > 50)
        {
            Instantiate(potionPrefab, new Vector2(vikingPosition.x + xDifferential, -3.95f), Quaternion.identity);
        }
        else if (chance > 25)
        {
            Instantiate(firePrefab, new Vector2(vikingPosition.x + xDifferential, -3.78f), Quaternion.identity);
        }
        else
        {
            Instantiate(arrowPrefab, new Vector2(vikingPosition.x + xDifferential, -3.08f), Quaternion.identity);
        }
    }

}
