using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNPC : NPC {

    private const int SLIME_MAX_HEALTH = 1;
    //private CharacterStats cs;
    private WeaponStats bodyHitbox;
    private PolygonCollider2D pc;
    private SpriteRenderer sre;

	protected override void Start ()
	{
        sre = gameObject.GetComponent<SpriteRenderer>();
        sre.flipX = true;
        //cs = gameObject.GetComponent<CharacterStats>();
        bodyHitbox = gameObject.GetComponent<WeaponStats>();
        pc = gameObject.GetComponent<PolygonCollider2D>();

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

        if (cs.hitstunLeft > 0)
        {
            ani.SetBool("hit", true);
        }
        else
        {
            ani.SetBool("hit", false);
        }

        if (cs.health <= 0)
        {
            pc.enabled = false;
            ani.SetBool("dead", true);
        }
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
        if (cs.hitstunLeft > 0 || cs.dying || cs.dead)
        {
            velocityX = 0;
            velocityY = 0;
        }
        else
        {
            velocityX = -5f;
        }
    }

}