using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ConnectAndTrigger connectAndTrigger;
    public int damage = 1;

    private void Awake()
    {
        gameObject.transform.localScale = Vector3.one * LevelManager.instance.explosionRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        LevelManager.instance.GainScore(enemy.score * connectAndTrigger.combo);
        enemy.TakeDamage(damage);
    }
}
