using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellNoFriends : SpellBase
{
    public int spawnAmount;
    public float spawnXMin = -1;
    public float spawnXMax = 1;
    public float spawnYMin = -1;
    public float spawnYMax = 1;

    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    private Vector3 spawnOffset;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            spawnOffset = new Vector3(Random.Range(spawnXMin, spawnXMax), Random.Range(spawnYMin, spawnYMax), 0);
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position + spawnOffset, Quaternion.identity);
            GameManager.Instance.AddEnemy(enemy.GetComponent<EntityBase>());
            GameManager.Instance.enemiesLeft++;
        }
        Destroy(gameObject);
    }
}
