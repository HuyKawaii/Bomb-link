using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject homeScreen;
    [SerializeField] Button[] upgradeButtoms;
    int numberOfButtom;
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] GameObject[] explains;
    int LevelMax = 5;
    public int upgradeIndex;
    [SerializeField] Animator baseAnimator;
    Color gold = new Color(1, 1, 0);
    Color select = new Color(0.5f, 0.5f, 0.5f);
    Color unSelect = new Color(1, 1, 1);
    int maxLevel = 5;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        numberOfButtom = upgradeButtoms.Length;
        upgradeIndex = -1;
    }
    private void Update()
    {
        coin.text = MainManager.instance.coin + "$";
        for (int i = 0; i < numberOfButtom; i++)
        {
            Upgrade upgrade = MainManager.instance.listOfUpgrade[i];
            TextMeshProUGUI level = upgradeButtoms[i].transform.Find("Level").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI price = upgradeButtoms[i].transform.Find("Price").GetComponent<TextMeshProUGUI>();
            if (upgrade.level == LevelMax)
            {
                level.color = gold;
                level.text = "MAX LEVEL";
                price.text = "";
            }
            else
            {
                level.text = "Lv " + (upgrade.level);
                price.text = upgrade.costPerLevel[upgrade.level] + "$";
            }

            if (i == upgradeIndex)
            {
                explains[i].SetActive(true);
                Image border = upgradeButtoms[i].GetComponent<Image>();
                border.color = select;
            }
            else
            {
                explains[i].SetActive(false);
                Image border = upgradeButtoms[i].GetComponent<Image>();
                border.color = unSelect;

            }
        }
    }

    public void ReturnHome()
    {
        AudioManager.instance.Play("Click");
        homeScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void upgradeEplosionRadius()
    {
        AudioManager.instance.Play("Click");
        upgradeIndex = 0;
    }

    public void upgradeExplosiveGain()
    {
        AudioManager.instance.Play("Click");
        upgradeIndex = 1;
    }
    
    public void upgradeExplosiveLimit()
    {
        AudioManager.instance.Play("Click");
        upgradeIndex = 2;
    }
    public void upgradeExplosiveCooldown()
    {
        AudioManager.instance.Play("Click");
        upgradeIndex = 3;
    }

    public void upgradeFuseBurningTime()
    {
        AudioManager.instance.Play("Click");
        upgradeIndex = 4;
    }
    public void upgradeComboLimit()
    {
        AudioManager.instance.Play("Click");
        upgradeIndex = 5;
    }

    public void upgradeButtom()
    {
        if (upgradeIndex >= 0)
        {
            Upgrade upgrade = MainManager.instance.listOfUpgrade[upgradeIndex];
            if (upgrade.level < maxLevel)
            {
                if (upgrade.costPerLevel[upgrade.level] <= MainManager.instance.coin)
                {
                    AudioManager.instance.Play("Explosion");
                    baseAnimator.Play("BaseShaking");
                    MainManager.instance.coin -= upgrade.costPerLevel[upgrade.level];
                    upgrade.level++;
                    MainManager.instance.SaveShopData();
                }
                else
                {
                    AudioManager.instance.Play("FailedClick");
                }
            }
            else
            {
                AudioManager.instance.Play("FailedClick");
            }
        }
    }
}
