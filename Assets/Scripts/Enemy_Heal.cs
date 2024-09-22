using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Heal : Enemy
{
    public float healAmount = 1.0f;

    protected override void Start()
    {
        base.Start();
        damage = 0;
    }
    public override void GetKilled ()
    {
        base.Die();
        Base.instance.HealBase(healAmount);
    }

    
}
