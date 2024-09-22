using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;
    [SerializeField] GameObject[] enemyType;
    float horizontalSpawnlimit = 9.0f;
    float spawnLocationX;
    float spawnLocationY = 5.5f;

    int hordesInWave = 4;                                                   //number of hordes in a wave
    int numberOfWave = 1;                                                   //number of wave in game
    WaveData[,] waves;                                                      //store wave data
    int wave = 0;
    int horde = 0;

    string waveDataPath = "Assets/Resources/wavedata.json";
    private void Awake()
    {
        EnemySpawnManager.instance = this;
        waves = new WaveData[numberOfWave, hordesInWave];
    }

    void Start()
    {
        InitWaves();
    }

    private void Update()
    {
        if (!LevelManager.instance.isGamePause)
        {
            SpawnEnemies();
        }
    }

    void InitWaves()
    {
        FileInfo sourceFile = new FileInfo(waveDataPath);
        StreamReader reader = sourceFile.OpenText();
        string data;

        for (int i = 0; i < numberOfWave; i++)
            for (int j = 0; j < hordesInWave; j++)
            {
                data = reader.ReadLine();           
                waves[i, j] = JsonUtility.FromJson<WaveData>(data);                 //get wave data from file
            }
    }

    void SpawnEnemies()
    {
        if (wave < numberOfWave)
        {
            if (!waves[wave, horde].isWaveOver)
                SpawnWave(waves[wave, horde]);
            else
                CheckWave();
        }
        else
            Debug.Log("You completed the game");
    }

    void CheckWave()
    {
        int enemyCount = 0;
        foreach (Transform child in transform)
            enemyCount++;
        if (enemyCount == 0)
            NextWave();
    }

    void SpawnWave(WaveData wave)
    {
        if (!wave.isWaveStarted)
        {
            foreach (EnemyInWave enemy in wave.enemyInWave)
                StartCoroutine(SpawnEnemyInWave(enemy));
            wave.isWaveStarted = true;
        }

        else if (!wave.isWaveOver)
        {
            wave.totalEnemyInWave = 0;
            foreach (EnemyInWave enemy in wave.enemyInWave)
                wave.totalEnemyInWave += enemy.numberOfEnemy;
            if (wave.totalEnemyInWave == 0)
                wave.isWaveOver = true;
        }
    }
    IEnumerator SpawnEnemyInWave(EnemyInWave enemy)
    {
        spawnLocationX = Random.Range(-horizontalSpawnlimit, horizontalSpawnlimit);
        Vector3 spawnLocation = new Vector3(spawnLocationX, spawnLocationY, 0);
        var newEnemy = Instantiate(enemyType[enemy.enemyTypeIndex], spawnLocation, Quaternion.identity);
        newEnemy.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(Random.Range(enemy.spawnTimeMin, enemy.spawnTimeMax));
        enemy.numberOfEnemy--;
        if (enemy.numberOfEnemy > 0)
            StartCoroutine(SpawnEnemyInWave(enemy));
    }

    void NextWave()
    {
        horde++;
        if (horde == hordesInWave)
        {
            horde = 0;
            wave++;
        }
    }
}
