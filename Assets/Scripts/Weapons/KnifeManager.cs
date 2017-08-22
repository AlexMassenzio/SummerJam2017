using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : PhysicsObject
{

    private GameObject mack;
    private Inventory inv;
    private WeaponStats ws;
    private SpriteRenderer sre;
    private SpriteRenderer knifeSR;

    protected override void Start()
    {
        base.Start();

        mack = GameObject.FindGameObjectWithTag("Player");
        inv = mack.GetComponentInChildren<Inventory>();
        ws = gameObject.GetComponent<WeaponStats>();
        sre = mack.GetComponentInChildren<SpriteRenderer>();
        knifeSR = gameObject.GetComponent<SpriteRenderer>();

        // If Mack is facing to the right
        if (!sre.flipX)
        {
            ws.initVelocity = new Vector2(0.5f, 27);
        }
        // If Mack is facing to the left
        else
        {
            knifeSR.flipX = true;
            ws.initVelocity = new Vector2(-0.5f, 27);
        }

        ws.damage = 2;
        ws.staminaCost = 10f;
        ws.cooldownMax = 1f;
        ws.hitstunDuration = 0.3334f;
        ws.useStunDuration = 0.3334f;
        ws.knockback = new Vector2();

        velocity = ws.initVelocity;
        if (inv != null)
        {
            inv.WeaponUsed(ws.cooldownMax, ws.useStunDuration);
        }
    }

    protected override void ComputeVelocity()
    {
        velocityX = ws.initVelocity.x;
    }

}