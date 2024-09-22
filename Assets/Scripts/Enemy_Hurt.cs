using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hurt : Enemy
{
    float killDamage = 1;
    public override void GetKilled()
    {
        base.Die();
        Base.instance.TakeDamage(killDamage);
    }
}
