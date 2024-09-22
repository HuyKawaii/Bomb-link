using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    float baseMaxHealth;
    Slider healthBar;
    

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
    }

    private void Start()
    {
        baseMaxHealth = Base.instance.baseMaxHealth;
    }
    void Update()
    {
        healthBar.value = Base.instance.baseHealth / baseMaxHealth;
    }
}
