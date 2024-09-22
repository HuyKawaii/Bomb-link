using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameObject playButtom;
    [SerializeField] GameObject pauseButtom;
    [SerializeField] TextMeshProUGUI cooldownCouter;
    [SerializeField] Image cooldownImage;

    [SerializeField] Animator gameOverText;
    [SerializeField] Animator gameOverScore;
    [SerializeField] Animator highScore;

    private void Awake()
    {
        UIManager.instance = this;
    }
    private void Update()
    {
        DisplayScore();
        DisplayCooldown();
    }

    void DisplayScore()
    {
        scoreText.text = "Score: " + LevelManager.instance.score;
    }

    void DisplayCooldown()
    {
        cooldownImage.fillAmount = ExplosiveSpawnManager.instance.cooldownTimer / ExplosiveSpawnManager.instance.cooldown;
        cooldownCouter.text = "" + ExplosiveSpawnManager.instance.explosiveCount;
    }
    public void UIPlay()
    {
        gameOverMenu.SetActive(false);
        scoreText.enabled = true;
        pauseButtom.SetActive(true);
    }

    public void UIGameOver()
    {
        scoreText.enabled = false;
        gameOverMenu.SetActive(true);
        pauseButtom.SetActive(false);
        PlayAnimation();
        gameOverScoreText.text = "Score: " + LevelManager.instance.score;
        highScoreText.text = "HIGHSCORE:" + LevelManager.instance.highScore;
    }

    public void UIPauseGame()
    {
        pauseButtom.SetActive(false);
        playButtom.SetActive(true);
        pauseMenu.SetActive(true);
    }

    public void UIContinueGame()
    {
        pauseButtom.SetActive(true);
        playButtom.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void PlayAnimation()
    {
        gameOverText.Play("GameOverText");
        gameOverScore.Play("GameOverScore");
        highScore.Play("HighScore");
    }
}
