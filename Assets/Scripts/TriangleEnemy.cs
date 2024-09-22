using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : Enemy
{
    private float warningTime;
    [SerializeField] GameObject warningSign;
    float triagleSpeed = 15;
    float warnedTime = 0f;
    float maxWarning = 2.5f;
    float minWarning = 2.0f;
    float flickerTime = 0.2f;
    float flickering = 0;
    float flickerDuration = 0;

    protected override void Start()
    {
        base.Start();
        RandomWarningTime();
    }

    protected override void Update()
    {
        MoveAfterWarning();
        CheckOutOfBound();
        Flicker();
    }
    protected override Vector3 RandomTrajectory()
    {
        return Vector3.down;
    }

    protected override float RandomSpeed()
    {
        return triagleSpeed;
    }

    private void RandomWarningTime()
    {
        warningTime = Random.Range(minWarning, maxWarning);
        gameObject.transform.eulerAngles = new Vector3(0, 0, 180); //just rotate enemy to the right direction
    }
    private void MoveAfterWarning()
    {
        if (warnedTime < warningTime)
            warnedTime += Time.deltaTime;
        else
        {
            warningSign.SetActive(false);
            enemyRigidbody.velocity = enemyTrajectory * enemySpeed;
        }
    }

    void Flicker()
    {
        if (flickerDuration < warningTime)
        {
            flickerDuration += Time.deltaTime;
            if (flickering < flickerTime)
                flickering += Time.deltaTime;
            else
            {
                warningSign.SetActive(!warningSign.activeSelf);
                flickering = 0;
            }
        }
        
    }
}
