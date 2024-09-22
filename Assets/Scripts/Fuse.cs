using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    ParticleSystem fuseParticle;
    LineRenderer lineRenderer;
    Vector3 fuseLength;
    public GameObject destination;
    public GameObject start;
    public static float fuseBuringTime = 0.5f;

    private void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        fuseParticle = gameObject.GetComponent<ParticleSystem>();
        fuseBuringTime = LevelManager.instance.fuseBurningTime;
    }
    private void Start()
    {
        GetComponent<LineRenderer>().SetPosition(0, start.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, destination.transform.position);
        fuseLength = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
    }
    private void Update()
    {
        if(destination != null && destination.layer == LayerManager.instance.burnLayer)
        {
            BurnFuseTo();
        }
        if(start.layer == LayerManager.instance.burnLayer)
        {
            BurnFuseFrom();
        }
    }
    public void BurnFuseTo()
    {
        Destroy(gameObject);
    }

    public void BurnFuseFrom()
    {
        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 newPos = Time.deltaTime / fuseBuringTime * fuseLength + startPos;
        fuseParticle.transform.position = newPos;
        lineRenderer.SetPosition(0, newPos);
        if (!fuseParticle.isPlaying)
            fuseParticle.Play();
    }
}
