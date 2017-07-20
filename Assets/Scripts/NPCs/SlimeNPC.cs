using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNPC : NPC {

	private const int SLIME_MAX_HEALTH = 1;
	private bool isAttacking = false;

	// Use this for initialization
	protected override void Start ()
	{
        damage = 5;
		health = SLIME_MAX_HEALTH;
		maxSpeed = 5f;
		cooldown = 5;
		
		base.Start();

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
