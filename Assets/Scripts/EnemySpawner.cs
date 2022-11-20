using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyObjects;
    public float spawnXmin = 0;
    public float spawnYmin = 0;
    public float spawnXmax = 20;
    public float spawnYmax = 20;

    private int enemyIndex;
    private Vector2 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            enemyIndex = Random.Range(0, enemyObjects.Length);
            Instantiate(enemyObjects[0], new Vector2(Random.Range(spawnXmin, spawnXmax), Random.Range(spawnYmin, spawnYmax)), transform.rotation);
        }
    }
}
