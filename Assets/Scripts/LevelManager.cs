using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public const string _EXPLOSION_RADIUS = "explosionRadius";
    public const string _EXPLOSION_GAIN_ON_KILL = "explosionGainOnKill";
    public const string _FUSE_BURNING_TIME = "fuseBurningTime";
    public const string _EXPLOSIVE_LIMIT = "explosiveLimit";
    public const string _EXPLOSIVE_COOLDOWN = "explosiveCooldown";
    public const string _COMBO_LIMIT = "comboLimit";

    [HideInInspector] public long score;
    [HideInInspector] public long highScore;
    [HideInInspector] public bool isGamePause = false;

    string scorePath;
    string shopDataPath;

    public float explosionRadius;
    float baseExplosionRadius = 0.5f;
    float radiusPerUpgrade = 0.1f;

    public float explosiveGainOnKill;
    float baseExplosiveGainOnKill = 1;
    float gainPerUpgrade = 0.5f;

    public float fuseBurningTime;
    float baseFuseBurningTime = 0.5f;
    float timePerUpgrade = -0.08f;

    public int explosiveLimit;
    int baseExplosiveLimit = 3;
    int limitPerUpgrade = 1;

    public float explosiveCooldown;
    float baseExplosiveCooldown = 2;
    float cooldownPerUpgrade = 0.2f;

    public int comboLimit = 10;

    private void Awake()
    {
        LevelManager.instance = this;
        scorePath = Application.persistentDataPath + "/saveScore.json";
        shopDataPath = Application.persistentDataPath + "/shop.json";
        LoadHighscore();
        LoadUpgrade();
    }

    void LoadHighscore()
    {
        if (File.Exists(scorePath))
        {
            string json = File.ReadAllText(scorePath);
            SaveScore savedData = new SaveScore();
            savedData = JsonUtility.FromJson<SaveScore>(json);
            highScore = savedData.highScore;
        }
    }

    void LoadUpgrade()
    {
        Upgrade[] listOfUpgrade = MainManager.instance.listOfUpgrade;
        explosionRadius = baseExplosionRadius + Array.Find(listOfUpgrade, upgrade => upgrade.name == _EXPLOSION_RADIUS).level * radiusPerUpgrade;
        explosiveCooldown = baseExplosiveCooldown + Array.Find(listOfUpgrade, upgrade => upgrade.name == _EXPLOSIVE_COOLDOWN).level * cooldownPerUpgrade;
        explosiveGainOnKill = baseExplosiveGainOnKill + Array.Find(listOfUpgrade, upgrade => upgrade.name == _EXPLOSION_GAIN_ON_KILL).level * gainPerUpgrade;
        explosiveLimit = baseExplosiveLimit + Array.Find(listOfUpgrade, upgrade => upgrade.name == _EXPLOSIVE_LIMIT).level * limitPerUpgrade;
        comboLimit = 10;
        fuseBurningTime = baseFuseBurningTime + Array.Find(listOfUpgrade, upgrade => upgrade.name == _FUSE_BURNING_TIME).level * timePerUpgrade;
    }

    void SaveHighscore()
    {
        if (score > highScore)
        {
            SaveScore newData = new SaveScore();
            highScore = score;
            newData.highScore = highScore;
            string json = JsonUtility.ToJson(newData);
            File.WriteAllText(scorePath, json);
        }
    }

    
    public void GainScore(int scoreGained)
    {
        score += scoreGained;
    }

    public void GameOver()
    {
        isGamePause = true;
        SaveHighscore();
        UIManager.instance.UIGameOver();
        MainManager.instance.coin += score;
        MainManager.instance.SaveShopData();
    }

    public void Play()
    {
        isGamePause = false;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        UIManager.instance.UIPlay();
        AudioManager.instance.Play("Click");
    }

    public void PauseGame()
    {
        isGamePause = true;
        Time.timeScale = 0;
        UIManager.instance.UIPauseGame();
        AudioManager.instance.Play("Click");
    }

    public void ContinueGame()
    {
        isGamePause = false;
        Time.timeScale = 1;
        AudioManager.instance.Play("Click");
        UIManager.instance.UIContinueGame();
    }

    public void Retry()
    {
        isGamePause = false;
        Time.timeScale = 1;
        AudioManager.instance.Play("Click");
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void HomeScreen()
    {
        isGamePause = false;
        Time.timeScale = 1;
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    [System.Serializable]
    class SaveScore
    {
        public long highScore;
    }

    
}
