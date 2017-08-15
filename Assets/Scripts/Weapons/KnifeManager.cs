using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : PhysicsObject
{

    private Inventory inv;
    private WeaponStats ws;

    protected override void Start()
    {
        base.Start();

        inv = gameObject.GetComponentInChildren<Inventory>();
        ws = gameObject.GetComponent<WeaponStats>();

        ws.initVelocity = new Vector2();

        ws.damage = 2;
        ws.staminaCost = 10f;
        ws.cooldownMax = 1f;
        ws.hitstunDuration = 0.3334f;
        ws.useStunDuration = 0.3334f;
        ws.knockback = new Vector2();

        velocity = ws.initVelocity;
        velocityX = ws.initVelocity.x;
        if (inv != null)
        {
            inv.WeaponUsed(ws.cooldownMax, ws.useStunDuration);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ComputeVelocity()
    {
        velocityX = 0.5f;
    }

}