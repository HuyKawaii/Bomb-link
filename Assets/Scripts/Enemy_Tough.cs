using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Enemy_Tough: Enemy
{
    [SerializeField] GameObject shield;
    private ParticleSystem shieldDestroyParticle;
    private SpriteRenderer shieldSpriteRenderer;
    private Light2D shieldGlow;
    protected override void Awake()
    {
        base.Awake();
        shieldDestroyParticle = shield.GetComponent<ParticleSystem>();
        shieldSpriteRenderer = shield.GetComponent<SpriteRenderer>();
        shieldGlow = shield.GetComponent<Light2D>();
    }

    protected override void Start()
    {
        base.Start();
        health = 2;
        enemySpeed = 1.0f;
    }

    public override void TakeDamage(float damageTaken)
    {
        base.TakeDamage(damageTaken);
        if (health > 0)
            DestroyShield();
    }

    private void DestroyShield()
    {
        shieldSpriteRenderer.enabled = false;
        shieldGlow.enabled = false;
        shieldDestroyParticle.Play();
    }   
}
