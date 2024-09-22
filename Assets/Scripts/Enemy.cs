using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    protected Light2D enemyGlow;
    protected Rigidbody2D enemyRigidbody;
    protected SpriteRenderer enemySprite;
    ParticleSystem enemyDieParticle;
    Collider2D enemyCollider;

    [HideInInspector] public float enemySpeed;
    protected Color enemyBaseColor = new Color32(250, 84, 74, 255);
    protected float enemySpeedMax = 4.5f;
    protected float enemySpeedMin = 5.5f;
    protected Vector3 enemyTrajectory;
    float horizontalBound = 9.0f;
    float verticalBound = 5.0f;
    float boundOffset = 1.0f;
    protected float health = 1.0f;
    public float damage = 1f;
    [HideInInspector] public int score = 1;

    protected virtual void Awake()
    {
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
        enemySprite = gameObject.GetComponent<SpriteRenderer>();
        enemyDieParticle = gameObject.GetComponent<ParticleSystem>();
        enemyCollider = gameObject.GetComponent<Collider2D>();
        enemyGlow = gameObject.GetComponent<Light2D>();
    }
    protected virtual void Start()
    {
        enemyTrajectory = RandomTrajectory();
        enemySpeed = RandomSpeed();
    }

    protected virtual void Update()
    {
        Move();
        CheckOutOfBound();
    }

    protected virtual Vector3 RandomTrajectory()
    {
        float aim = Random.Range(-horizontalBound, horizontalBound);
        Vector3 enemyDirection = new Vector3(aim, -verticalBound, 0) - transform.position;
        return enemyDirection.normalized;
    }

    protected virtual float RandomSpeed()
    {
        return Random.Range(enemySpeedMin, enemySpeedMax);
    }
    protected void Move()
    {
        Vector3 enemyVelocity = enemySpeed * enemyTrajectory;
        enemyRigidbody.velocity = enemyVelocity;
    }

    protected void CheckOutOfBound()
    {
        if (transform.position.x > horizontalBound+boundOffset || transform.position.x < -(horizontalBound+boundOffset) || transform.position.y < -verticalBound)
            Destroy(gameObject);
    }

    public virtual void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
            GetKilled();
    }
    public virtual void Die()
    {
        enemySpeed = 0;
        enemySprite.enabled = false;
        enemyCollider.enabled = false;
        enemyGlow.enabled = false;
        enemyDieParticle.Play();
        AudioManager.instance.Play("EnemyDie");
    }

    public virtual void GetKilled()
    {
        Die();
        ExplosiveSpawnManager.instance.ResetCooldownOnKill();
    }
    
}
