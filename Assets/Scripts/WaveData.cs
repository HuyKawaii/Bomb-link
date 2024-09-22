using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public bool isWaveOver = false;
    public bool isWaveStarted = false;
    public int totalEnemyInWave = 0;
    public EnemyInWave[] enemyInWave;
}

[System.Serializable]
public class EnemyInWave
{
    public int enemyTypeIndex = -1;
    public int numberOfEnemy = 0;
    public float spawnTimeMin = 0;
    public float spawnTimeMax = 0;
}