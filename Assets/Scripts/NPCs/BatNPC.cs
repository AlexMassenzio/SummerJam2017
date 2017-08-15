using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatNPC : NPC
{

    private const int BAT_MAX_HEALTH = 10;
    private WeaponStats bodyHitbox;

    protected override void Start()
    {

        bodyHitbox = gameObject.GetComponent<WeaponStats>();

        bodyHitbox.knockback = new Vector2(2f, 5f);
        bodyHitbox.damage = 5;
        bodyHitbox.hitstunDuration = 0.5f;

        health = BAT_MAX_HEALTH;
        maxSpeed = 5f;

        base.Start();

        SetTarget(GameObject.FindGameObjectWithTag("Player"));
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ComputeVelocity()
    {
        //gravityModifier = 0;
        velocityX = -5f;
    }

}