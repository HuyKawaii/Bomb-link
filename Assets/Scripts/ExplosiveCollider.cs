using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCollider : MonoBehaviour
{
    [SerializeField] GameObject explosionArea;
    [SerializeField] GameObject connectArea;
    private ConnectAndTrigger connectAndTrigger;
    public bool inBase = false;
    bool isTriggered = false;
    Vector3 smallScale = new Vector3(0f, 0f, 0f);
    Vector3 normalScale = new Vector3(0.25f, 0.25f, 0.25f);
    [SerializeField] float growRate;

    private void Awake()
    {
        connectAndTrigger = connectArea.GetComponent<ConnectAndTrigger>();
    }
    private void Start()
    {
        gameObject.transform.localScale = smallScale;
    }

    private void Update()
    {
        GrowOnAppear();
    }
    private void OnMouseDown()
    {
        if(inBase && !LevelManager.instance.isGamePause && !isTriggered)
        {
            connectAndTrigger.Trigger();
            isTriggered = true;
        }
    }
    private void GrowOnAppear()
    {
        if (gameObject.transform.localScale.x < normalScale.x)
            Grow();
        else if (gameObject.transform.localScale.x > normalScale.x)
            gameObject.transform.localScale = normalScale;
    }
    private void Grow()
    {
        float currentScale = gameObject.transform.localScale.x;
        gameObject.transform.localScale = new Vector3(currentScale + Time.deltaTime * growRate, currentScale + Time.deltaTime * growRate, currentScale + Time.deltaTime * growRate);
    }
}
