using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Base : MonoBehaviour
{
    public static Base instance;
    [SerializeField] ParticleSystem healthBarParticle;

    Light2D baseGlow;
    SpriteRenderer baseRenderer;
    ParticleSystem baseParticle;
    public float baseMaxHealth = 10.0f;
    public float baseHealth;
    Animator baseAnimator;

    private void Awake()
    {
        Base.instance = this;
        baseGlow = GetComponent<Light2D>();
        baseAnimator = GetComponent<Animator>();
        baseRenderer = GetComponent<SpriteRenderer>();
        baseParticle = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        baseHealth = baseMaxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerManager.instance.explosiveLayer)
            collision.gameObject.GetComponent<ExplosiveCollider>().inBase = true;
        if (collision.gameObject.layer == LayerManager.instance.enemyLayer)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            TakeDamage(enemy.damage);
            enemy.Die();
        }
    }

    public void TakeDamage(float damageTaken)
    {
        if (baseHealth > 0)
            baseHealth -= damageTaken;
        Debug.Log("Base is hurt");
        baseAnimator.Play("BaseShaking");
        healthBarParticle.Play();
        if (baseHealth <= 0)
            BaseDestroyed();
    }
    
    public void HealBase(float amountHealed)
    {
        baseHealth += amountHealed;
        if (baseHealth > baseMaxHealth)
            baseHealth = baseMaxHealth;
    }

    void BaseDestroyed()
    {
        LevelManager.instance.GameOver();
        Collider2D baseCollider = gameObject.GetComponent<Collider2D>();
        baseCollider.enabled = false;
        baseRenderer.enabled = false;
        baseGlow.enabled = false;
        baseParticle.Play();
        AudioManager.instance.Play("BaseDestroyed");
    }
}


