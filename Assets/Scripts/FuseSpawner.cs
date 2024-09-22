using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseSpawner : MonoBehaviour
{
    [SerializeField] GameObject fusePrefab;

    public void SpawnFuse(GameObject start, GameObject destination)
    {
        GameObject fuse = Instantiate(fusePrefab, transform.position, Quaternion.identity, transform);
        fuse.GetComponent<Fuse>().destination = destination;
        fuse.GetComponent<Fuse>().start = start;
        
    }
}
