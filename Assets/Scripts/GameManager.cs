using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [NonSerialized]
    public int enemiesLeft;
    [NonSerialized]
    public WaveSpawner.Wave currentWave;
    [NonSerialized]
    public bool isBossAlive;

    [NonSerialized]
    public List<EntityBase> enemiesAlive;

    public string nextScene;
    public string transitionText;

    private void Awake()
    {
        Instance = this;
        enemiesAlive = new List<EntityBase>();
    }

    public void AddEnemy(EntityBase entity)
    {
        enemiesAlive.Add(entity);
    }

    public void AddEnemyWithCount(EntityBase entity)
    {
        enemiesAlive.Add(entity);
        enemiesLeft++;
    }

    public void RemoveEnemy(EntityBase entity)
    {
        enemiesAlive.Remove(entity);
        enemiesLeft--;
    }

    public void SceneOver(bool isWon)
    {
        if (isWon)
        {
            if (nextScene == "Game Over Win")
            {
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                NavigationDataHolder.NextSceneToLoad = nextScene;
                NavigationDataHolder.TransitionSceneText = transitionText;
                SceneManager.LoadScene("Transition");
            }
        }
        else
        {
            SceneManager.LoadScene("Game Over Lose");
        }
    }
}
