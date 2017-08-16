﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSlimeNPC : NPC
{

    private const int SLIME_MAX_HEALTH = 1;
    private WeaponStats bodyHitbox;
    private float jumpWaitStart;
    public float secondsToJump = 3f;

    protected override void Start()
    {

        jumpWaitStart = Time.time;

        bodyHitbox = gameObject.GetComponent<WeaponStats>();

        bodyHitbox.knockback = new Vector2(2f, 5f);
        bodyHitbox.damage = 5;
        bodyHitbox.hitstunDuration = 0.5f;

        health = SLIME_MAX_HEALTH;
        maxSpeed = 5f;

        base.Start();

        SetTarget(GameObject.FindGameObjectWithTag("Player"));
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {

        if (Time.time - jumpWaitStart > secondsToJump)
        {
            if (grounded)
            {
                velocity = new Vector2(velocity.x, Vector2.up.y * 20);
                Debug.Log("jumped at " + Time.time);
            }
            jumpWaitStart = Time.time;
        }

        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = velocityX;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, 'x');

        move = Vector2.up * deltaPosition.y;

        Movement(move, 'y');
    }

    protected override void Action()
    {

    }

    protected override void ComputeVelocity()
    {
        // Just move to the left at a constant speed
        velocityX = -5f;
    }

}