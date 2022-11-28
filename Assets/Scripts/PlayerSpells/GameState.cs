using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game State", menuName = "Game State")]
public class GameState : ScriptableObject
{
    public int enemiesLeft;
    public int currentLevel;
    public WaveSpawner.Wave currentWave;
    public bool isBossAlive;

    public List<EntityBase> enemiesAlive;
}
