using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnXmin = -2;
    public float spawnYmin = -2;
    public float spawnXmax = 2;
    public float spawnYmax = 2;

    [SerializeField]
    private GameState gameState;
    private Vector3 spawnPos;
    private GameObject enemySpawned;

    public void SpawnEnemy(GameObject enemy)
    {
        spawnPos = transform.position;
        spawnPos.x += Random.Range(spawnXmin, spawnXmax);
        spawnPos.y += Random.Range(spawnYmin, spawnYmax);
        enemySpawned = Instantiate(enemy, spawnPos, Quaternion.identity);
        gameState.enemiesAlive.Add(enemySpawned.GetComponent<EntityBase>());
    }
}
