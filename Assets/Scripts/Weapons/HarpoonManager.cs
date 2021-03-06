﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonManager : MonoBehaviour
{

    public int damage = 5;
    public float hitstunDuration = 0.5f;

    public bool attacking = false;
    public bool delayEnabled = false;
    public bool timeEnabled = false;

    public float attackDelayMax = 0.1f;
    public float attackDelayLeft = 0;
    public float attackTimeMax = 0.65f;
    public float attackTimeLeft = 0;

    public Collider2D attackTrigger;
    private PlayerManager pm;
    private WeaponStats ws;

    private void Start()
    {
        pm = gameObject.GetComponentInChildren<PlayerManager>();
        ws = gameObject.GetComponentInChildren<WeaponStats>();

        ws.damage = this.damage;
        ws.hitstunDuration = this.hitstunDuration;
    }

    private void Awake()
    {
        // Default to having the hitbox disabled
        attackTrigger.enabled = false;
    }

    private void Update()
    {

        if (pm.hasHarpoon)
        {
            attackTrigger = transform.GetChild(2).GetComponent<BoxCollider2D>();
        }

        // GetMouseButtonDown(0) is left click, 1 is right, and 2 is middle
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            attacking = true;
            delayEnabled = true;
            attackDelayLeft = attackDelayMax;
        }

        if (attacking)
        {
            if (delayEnabled)
            {
                if (attackDelayLeft > 0)
                {
                    attackDelayLeft -= Time.deltaTime;
                }
                else
                {
                    delayEnabled = false;
                    timeEnabled = true;
                    attackTimeLeft = attackTimeMax;
                    attackTrigger.enabled = true;
                }
            }
            else if (timeEnabled)
            {
                if (attackTimeLeft > 0)
                {
                    attackTimeLeft -= Time.deltaTime;
                }
                else
                {
                    attacking = false;
                    timeEnabled = false;
                    attackTrigger.enabled = false;
                }
            }
        }
    }
}
