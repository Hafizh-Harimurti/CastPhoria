using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WaveSpawner;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<GameObject> enemies;
        public List<int> amounts;
        public float spawnRate;
    }

    public Wave[] waves;
    private int currentWave = 0;
    private int nextWave = 0;
    private bool isReady;
    [SerializeField]
    private GameState gameState;
    [SerializeField]
    private GameObject bossBar;

    [SerializeField]
    private List<GameObject> spawnLocations;

    private List<EnemySpawner> enemySpawners;
    private int enemyIndex;
    private int spawnerIndex;
    private void Start()
    {
        enemySpawners = new List<EnemySpawner>();
        foreach(GameObject spawnLocation in spawnLocations)
        {
            enemySpawners.Add(spawnLocation.GetComponent<EnemySpawner>());
        }
        isReady = true;
    }

    private void Update()
    {
        if (isReady)
        {
            bossBar.SetActive(false);
            isReady = false;
            StartCoroutine(SpawnWave(waves[currentWave]));
            nextWave++;
        }
        else if (!gameState.isBossAlive && gameState.currentWave.name.Contains("Boss"))
        {
            bossBar.SetActive(false);
            foreach (EntityBase entity in gameState.enemiesAlive)
            {
                entity.isDead = true;
            }
            gameState.GameOver(true);
        }
        else if (!gameState.currentWave.name.Contains("Boss") && gameState.enemiesLeft  <= 0)
        {
            currentWave = nextWave;
            isReady = true;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        gameState.currentWave = wave;
        gameState.enemiesLeft = wave.amounts.Sum();
        if (wave.name == "Boss Wave")
        {
            gameState.isBossAlive = true;
            bossBar.SetActive(true);
        }
        while(wave.amounts.Count > 0)
        {
            enemyIndex = Random.Range(0, wave.amounts.Count);
            spawnerIndex = Random.Range(0, enemySpawners.Count);
            enemySpawners[spawnerIndex].SpawnEnemy(wave.enemies[enemyIndex]);
            wave.amounts[enemyIndex]--;
            if (wave.amounts[enemyIndex] <= 0)
            {
                wave.amounts.RemoveAt(enemyIndex);
                wave.enemies.RemoveAt(enemyIndex);
            }
            yield return new WaitForSeconds(wave.spawnRate);
        }

    }
}
