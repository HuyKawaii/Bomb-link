using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ConnectAndTrigger : MonoBehaviour
{
    [SerializeField] GameObject explosionArea;
    [SerializeField] GameObject explosive;
    [SerializeField] FuseSpawner fuseSpawner;
    [SerializeField] Light2D explosiveGlow;
    ParticleSystem explodeParticle;
    float explodingTime = 0.5f;
    public int combo = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerManager.instance.burnLayer)
        {
            StartCoroutine(WaitForFuse());
            IncreaseCombo(collision.gameObject);
        }
        else if(collision.gameObject.layer == LayerManager.instance.connectLayer)
        {
            fuseSpawner.SpawnFuse(gameObject, collision.gameObject);
        }
    }

    void IncreaseCombo(GameObject previousExplosive)
    {
        combo = previousExplosive.GetComponent<ConnectAndTrigger>().combo + 1;
        if (combo > LevelManager.instance.comboLimit)
            combo = LevelManager.instance.comboLimit;

    }
    IEnumerator WaitForFuse()
    {
        yield return new WaitForSeconds(LevelManager.instance.fuseBurningTime);
        Trigger();
    }
    public void Trigger()
    {
        Explode();
        BurnFuses();
    }

    void BurnFuses()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.layer = LayerManager.instance.burnLayer;
        gameObject.GetComponent<Collider2D>().enabled = true;
        StartCoroutine(BurnLag());
    }

    void Explode()
    {
        explosionArea.SetActive(true);
        explosive.GetComponent<SpriteRenderer>().enabled = false;
        explosiveGlow.enabled = false;
        AudioManager.instance.Play("Explosion");
        StartCoroutine(Exploding());
    }

    IEnumerator Exploding()
    {
        yield return new WaitForSeconds(explodingTime);
        Destroy(transform.parent.gameObject);
    }

    IEnumerator BurnLag()
    {
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
