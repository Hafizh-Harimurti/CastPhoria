using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        else if (GameManager.Instance.enemiesLeft <= 0)
        {
            if (nextWave < waves.Length)
            {
                currentWave = nextWave;
                isReady = true;
            }
            else
            {
                GameManager.Instance.SceneOver(true);
            }
        }
        if (!GameManager.Instance.isBossAlive && !GameManager.Instance.currentWave.name.Contains("Normal"))
        {
            foreach (EntityBase entity in GameManager.Instance.enemiesAlive)
            {
                entity.isDead = true;
            }
            
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        GameManager.Instance.currentWave = wave;
        GameManager.Instance.enemiesLeft = wave.amounts.Sum();
        if (!wave.name.Contains("Normal"))
        {
            GameManager.Instance.isBossAlive = true;
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
