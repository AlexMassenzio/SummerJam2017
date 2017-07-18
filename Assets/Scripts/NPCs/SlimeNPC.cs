using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNPC : NPC {

	public Collider2D attackTrigger;
	public GameObject attackPos;

	private const int SLIME_MAX_HEALTH = 1;

	private bool isAttacking = false;
	private float cooldown = 5;

	// Use this for initialization
	void Start ()
	{
		health = SLIME_MAX_HEALTH;
		SetTarget(GameObject.FindGameObjectWithTag("Player"));
		myDamageInfo = new DamageInfo(5);
	}

	protected override void Update()
	{
		base.Update();

		// Because of the rising glitch, we have to track position manually
		attackTrigger.transform.position = attackPos.transform.position;

	}

	protected override void Action()
	{
		
	}

	protected override void ComputeVelocity()
	{
		
	}
}
