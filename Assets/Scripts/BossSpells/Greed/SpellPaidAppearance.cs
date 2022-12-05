using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPaidAppearance : SpellBase
{
    public List<GameObject> coins;
    public float spawnXMin = -0.5f;
    public float spawnXMax = 0.5f;
    public float spawnYMin = -0.5f;
    public float spawnYMax = 0.5f;

    public List<GameObject> enemies;

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(spawnXMin, spawnXMax), Random.Range(spawnYMin, spawnYMax), 0);
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position + spawnOffset, Quaternion.identity);
            GameManager.Instance.AddEnemy(enemy.GetComponent<EntityBase>());
        }
        SpellCoin coin;
        foreach (GameObject coinGameObject in coins)
        {
            coin = coinGameObject.GetComponent<SpellCoin>();
            coin.StartExplosion();
        }
        Destroy(gameObject);
    }
}
