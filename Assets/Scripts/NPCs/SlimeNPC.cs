using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNPC : NPC {

    private CharacterStats cs;

	private const int SLIME_MAX_HEALTH = 1;

	private bool isAttacking = false;
	private float cooldown = 5;

	// Use this for initialization
	protected override void Start ()
	{
        base.Start();

        cs = gameObject.GetComponent<CharacterStats>();
        cs.health = SLIME_MAX_HEALTH;
        cs.myDamageInfo = new DamageInfo(5);
        //cs.maxSpeed = maxSpeed;
        //cs.crouchSpeed = crouchSpeed;
        //cs.jumpTakeOffSpeed = jumpTakeOffSpeed;

        SetTarget(GameObject.FindGameObjectWithTag("Player"));
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void Action()
	{
		
	}

	protected override void ComputeVelocity()
	{
		
	}

}
