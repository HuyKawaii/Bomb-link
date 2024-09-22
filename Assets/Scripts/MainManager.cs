using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    const string _EXPLOSION_RADIUS = "explosionRadius";
    const string _EXPLOSION_GAIN_ON_KILL = "explosionGainOnKill";
    const string _FUSE_BURNING_TIME = "fuseBurningTime";
    const string _EXPLOSIVE_LIMIT = "explosiveLimit";
    const string _EXPLOSIVE_COOLDOWN = "explosiveCooldown";
    const string _COMBO_LIMIT = "comboLimit";

    public Upgrade[] listOfUpgrade;
    public long coin;
    string shopDataPath;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            MainManager.instance = this;
            shopDataPath = Application.persistentDataPath + "/upgrade.json";
            Debug.Log(shopDataPath);
            if (!LoadShopData())
                InitShopData();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void InitShopData()
    {
        coin = 0;

        listOfUpgrade = new Upgrade[5];

        listOfUpgrade[0] = new Upgrade();
        listOfUpgrade[0].level = 0;
        listOfUpgrade[0].costPerLevel = new int[] { 50, 100, 200, 400, 600, 800 };
        listOfUpgrade[0].name = _EXPLOSION_RADIUS;

        listOfUpgrade[1] = new Upgrade();
        listOfUpgrade[1].level = 0;
        listOfUpgrade[1].costPerLevel = new int[] { 50, 150, 450, 1350, 3000 };
        listOfUpgrade[1].name = _EXPLOSION_GAIN_ON_KILL;

        listOfUpgrade[2] = new Upgrade();
        listOfUpgrade[2].level = 0;
        listOfUpgrade[2].costPerLevel = new int[] { 45, 90, 180, 360, 720, 1440 };
        listOfUpgrade[2].name = _EXPLOSIVE_LIMIT;

        listOfUpgrade[3] = new Upgrade();
        listOfUpgrade[3].level = 0;
        listOfUpgrade[3].costPerLevel = new int[] { 25, 50, 100, 200, 400, 800};
        listOfUpgrade[3].name = _EXPLOSIVE_COOLDOWN;

        listOfUpgrade[4] = new Upgrade();
        listOfUpgrade[4].level = 0;
        listOfUpgrade[4].costPerLevel = new int[] { 20, 40, 80, 160, 320, 640 };
        listOfUpgrade[4].name = _FUSE_BURNING_TIME;

        //listOfUpgrade[5] = new Upgrade();
        //listOfUpgrade[5].level = 0;
        //listOfUpgrade[5].costPerLevel = new int[] { 75, 150, 300, 600, 1200 };
        //listOfUpgrade[5].name = _COMBO_LIMIT;

        SaveShopData();
    }
    
    bool LoadShopData()
    {
        if (File.Exists(shopDataPath))
        {
            string json;
            json = File.ReadAllText(shopDataPath);
            ShopData shopData = JsonUtility.FromJson<ShopData>(json);
            coin = shopData.coin;
            listOfUpgrade = shopData.listOfUpgrade;
            return true;
        }
        else
            return false;
    }

    public void SaveShopData()
    {
        ShopData shopData = new ShopData();
        shopData.coin = coin;
        shopData.listOfUpgrade = listOfUpgrade;
        string json = JsonUtility.ToJson(shopData);
        File.WriteAllText(shopDataPath, json);
    }

    
}
