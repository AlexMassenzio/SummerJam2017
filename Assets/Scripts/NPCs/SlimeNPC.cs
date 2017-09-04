using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNPC : NPC {

    private const int SLIME_MAX_HEALTH = 1;
    private WeaponStats bodyHitbox;
    private BoxCollider2D bc;
    private SpriteRenderer sre;
    private Animator anim;
    private GameObject character;

	protected override void Start ()
	{
        character = GameObject.FindGameObjectWithTag("Character");
        anim = gameObject.GetComponent<Animator>();
        sre = gameObject.GetComponent<SpriteRenderer>();
        sre.flipX = true;
        bodyHitbox = gameObject.GetComponent<WeaponStats>();
        bc = gameObject.GetComponent<BoxCollider2D>();

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

        // Basic AI that will reverse direction if running into a wall

        Vector2[] origins = new Vector2[4];
        RaycastHit2D[] hits = new RaycastHit2D[4];
        float xBaseOffset = 1.7f;
        float yBaseOffset = -0.5f;
        float checkDistance = 0.1f;
        Vector2 checkDirection = Vector2.right;

        // If we're facing left
        if (sr.flipX)
        {
            xBaseOffset = -xBaseOffset;
            checkDirection = Vector2.left;
        }

        for (int i = 0; i < 4; i++)
        {
            origins[i] = new Vector2(transform.position.x + xBaseOffset, transform.position.y + yBaseOffset);
            yBaseOffset -= 0.5f;
            hits[i] = Physics2D.Raycast(origins[i], checkDirection, checkDistance);
            Debug.DrawLine(origins[i], new Vector2(origins[i].x + checkDistance, origins[i].y));
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Wall")
            {
                Debug.Log("Detected wall");
                sr.flipX = !sr.flipX;
                cs.maxSpeed *= -1;
                break;
            }
            // If none of the triggers hit anything
            else if (i == 5)
            {
                Debug.Log("Not hitting anything");
            }
        }

        base.Update();

        if (cs.hitstunLeft > 0)
        {
            anim.SetBool("hit", true);
        }
        else
        {
            anim.SetBool("hit", false);
        }

        if (cs.health <= 0 || Vector2.Distance(character.transform.position, gameObject.transform.position) > 100)
        {
            bc.enabled = false;
            sr.flipX = false;
            anim.SetBool("dead", true);
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
            velocityX = -cs.maxSpeed;
        }
    }

}