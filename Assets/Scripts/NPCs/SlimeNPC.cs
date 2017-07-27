using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNPC : NPC {

	private const int SLIME_MAX_HEALTH = 1;
	private bool isAttacking = false;

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

    protected override void FixedUpdate()
    {
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
        velocityX = -5;
    }

}
