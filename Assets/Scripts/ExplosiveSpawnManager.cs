using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveSpawnManager : MonoBehaviour
{
    public static ExplosiveSpawnManager instance;
    [SerializeField] GameObject explosive;
    public int explosiveLimit;
    public int explosiveCount;
    public float cooldown;
    public float cooldownTimer;
    private void Awake()
    {
        ExplosiveSpawnManager.instance = this;
        
    }
    private void Start()
    {
        explosiveLimit = LevelManager.instance.explosiveLimit;
        cooldown = LevelManager.instance.explosiveCooldown;
        explosiveCount = explosiveLimit;
        cooldownTimer = cooldown;
    }
    void Update()
    {
        if(!LevelManager.instance.isGamePause)
        {
            SpawnAtMouseClick();
            Cooldown();
        }
    }

    void SpawnAtMouseClick()
    {
        if (Input.GetMouseButtonDown(0) && explosiveCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider == null || (hit.collider.gameObject.layer != LayerManager.instance.explosiveLayer && hit.collider.gameObject.layer != LayerManager.instance.uiLayer))
            {
                Vector3 mousePositon = Input.mousePosition;
                mousePositon.z = 0;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePositon);
                worldPosition.z = 0;
                Instantiate(explosive, worldPosition, Quaternion.identity);
                explosiveCount--;
            }
        }
    }

    void Cooldown()
    {
        if (explosiveCount < explosiveLimit)
            cooldownTimer -= Time.deltaTime;
        else
            cooldownTimer = cooldown;

        if (cooldownTimer < 0)
        {
            ResetCooldown();
        }
    }

    void ResetCooldown()
    {
        int roundReseted = (int) Mathf.Floor(-cooldownTimer / cooldown);
        explosiveCount += roundReseted + 1;
        if (explosiveCount > explosiveLimit)
            explosiveCount = explosiveLimit;
        cooldownTimer = cooldown + (cooldownTimer + roundReseted * cooldown);
    }

    public void ResetCooldownOnKill()
    {
        cooldownTimer -= LevelManager.instance.explosiveGainOnKill * cooldown;
    }

}
